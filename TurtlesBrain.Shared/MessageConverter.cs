using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace TurtlesBrain.Shared
{
    public delegate int Writer(ref byte[] buffer, ITurtleApiMessage message);
    public delegate ITurtleApiMessage Reader(byte[] buffer, int len);
    public static class MessageConverter
    {
        private static readonly Dictionary<int, Reader> Readers = new Dictionary<int, Reader>();
        private static readonly Dictionary<int, Writer> Writers = new Dictionary<int, Writer>();

        private static readonly Dictionary<Type, int> TypeMap = new Dictionary<Type, int>();

        public static ITurtleApiMessage Read(int messageType) => Read(messageType, null, 0);
        public static ITurtleApiMessage Read(int messageType, byte[] buffer, int read) => Readers[messageType](buffer, read);

        private static void EnsureBufferSize(ref byte[] buffer, int pos, int toWrite) 
            => EnsureBufferSize(ref buffer, pos + toWrite);
        public static void EnsureBufferSize(ref byte[] buffer, int targetSize, bool growByRate = false)
        {
            if (buffer.Length >= targetSize)
                return;

            var tmp = new byte[growByRate ? (int)(buffer.Length * 1.4) <= targetSize ? targetSize : (int)(buffer.Length * 1.4) : targetSize];
            Buffer.BlockCopy(buffer, 0, tmp, 0, buffer.Length);
            buffer = tmp;
        }


        public static void Write(ITurtleApiMessage msg, ref byte[] buffer, out int messageType, out int len)
        {
            messageType = TypeMap[msg.GetType()];
            len = Writers[messageType](ref buffer, msg);
        }

        public static void Initialize()
        {
            var baseMsgType = typeof(ITurtleApiMessage);
            var msgTypes = baseMsgType.Assembly.GetTypes()
                .Where(t => !t.IsAbstract && baseMsgType.IsAssignableFrom(t))
                .ToList();

            int identifier = 0;

            foreach (var msgType in msgTypes.OrderBy(t => t.FullName))
            {
                msgType.GetField("MessageType", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                    .SetValue(null, ++identifier, BindingFlags.Static | BindingFlags.FlattenHierarchy, null, CultureInfo.InvariantCulture);

                var properties = msgType.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                    .OrderBy(p => p.Name)
                    .ToList();

                Writers[identifier] = MakeWriter(msgType, properties);
                Readers[identifier] = MakeReader(msgType, properties);
                TypeMap[msgType] = identifier;
            }
        }

        private static Reader MakeReader(Type msgType, List<PropertyInfo> properties)
        {
            var bufferParam = Expression.Parameter(typeof(byte[]));
            var typeVar = Expression.Variable(msgType);
            var readParam = Expression.Parameter(typeof(int));

            var variables = new List<ParameterExpression> { typeVar };
            var expressions = new List<Expression> { Expression.Assign(typeVar, Expression.New(msgType)) };

            if (properties.Count > 0)
            {
                var posVar = Expression.Variable(typeof(int));
                var tmpBytes = Expression.Variable(typeof(byte[]));
                var tmpLen = Expression.Variable(typeof(int));

                expressions.Add(Expression.Assign(posVar, Expression.Constant(0)));

                variables.Add(tmpBytes);
                variables.Add(tmpLen);
                variables.Add(posVar);

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(int))
                    {
                        var conversionMethod = typeof(BitConverter).GetMethod("ToInt32");
                        expressions.Add(Expression.Block(
                            Expression.Call(typeVar, property.SetMethod, Expression.Call(conversionMethod, bufferParam, posVar)),
                            Expression.AddAssign(posVar, Expression.Constant(4))
                        ));
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        var lenConv = typeof(BitConverter).GetMethod("ToInt32");
                        var strConv = typeof(UTF8Encoding).GetMethod("GetString", new[] { typeof(byte[]), typeof(int), typeof(int) });

                        expressions.Add(Expression.Block(
                            Expression.Assign(tmpLen, Expression.Call(lenConv, bufferParam, posVar)),
                            Expression.AddAssign(posVar, Expression.Constant(4)),
                            Expression.IfThen(
                                Expression.GreaterThan(tmpLen, Expression.Constant(0)),
                                Expression.Block(
                                    Expression.Call(typeVar, property.SetMethod,
                                        Expression.Call(Expression.Constant(Encoding.UTF8), strConv, bufferParam, posVar, tmpLen)
                                    ),
                                    Expression.AddAssign(posVar, tmpLen)
                                )
                            )
                        ));
                    }

                }
            }

            expressions.Add(typeVar);

            return Expression.Lambda<Reader>(
                Expression.Block(variables, expressions),
                bufferParam, readParam).Compile();
        }

        private static Writer MakeWriter(Type msgType, List<PropertyInfo> properties)
        {
            var byteArrRefType = typeof(byte[]).MakeByRefType();
            var baseParam = Expression.Parameter(typeof(ITurtleApiMessage), "msg");
            var bufferParam = Expression.Parameter(byteArrRefType, "buffer");

            var typeVar = Expression.Variable(msgType, "actualMsg");
            var posVar = Expression.Variable(typeof(int), "pos");
            var lenVar = Expression.Variable(typeof(int), "len");

            var variables = new List<ParameterExpression> {
                posVar,
                lenVar,
                typeVar
            };


            var expressions = new List<Expression> {
                Expression.Assign(typeVar, Expression.Convert(baseParam, msgType)),
                Expression.Assign(posVar, Expression.Constant(8))
            };

            var bufferSizeMethod = typeof(MessageConverter).GetMethod(nameof(EnsureBufferSize), BindingFlags.NonPublic | BindingFlags.Static, null, new[] { byteArrRefType, typeof(int), typeof(int) }, null);

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    expressions.Add(WriteInt32(bufferParam, bufferSizeMethod, posVar, Expression.Property(typeVar, property)));
                }
                else if (property.PropertyType == typeof(string))
                {
                    expressions.Add(WriteString(bufferParam, bufferSizeMethod, posVar, Expression.Property(typeVar, property), lenVar));
                }
            }

            expressions.Add(posVar);

            return Expression.Lambda<Writer>(
                Expression.Block(variables, expressions),
                bufferParam,
                baseParam
                ).Compile();
        }

        private static Expression WriteInt32(Expression bufferParam, MethodInfo bufferSizeMethod, Expression pos, Expression value)
        {           
            return Expression.Block(
                Expression.Call(bufferSizeMethod, bufferParam, pos, Expression.Constant(4)),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(value, typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(8)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(16)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(24)), typeof(byte))),
                pos
            );
        }

        private static Expression WriteInt64(Expression bufferParam, MethodInfo bufferSizeMethod, Expression pos, Expression value)
        {
            return Expression.Block(
                Expression.Call(bufferSizeMethod, bufferParam, pos, Expression.Constant(8)),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(value, typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(8)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(16)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(24)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(32)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(40)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(48)), typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(56)), typeof(byte))),
                pos
            );
        }

        private static Expression WriteString(Expression bufferParam, MethodInfo bufferSizeMethod, Expression pos, Expression value, Expression lenVar)
        {

            var lenProp = typeof(string).GetProperty("Length");
            var charCount = typeof(UTF8Encoding).GetMethod("GetByteCount", new[] { typeof(string) });
            var strConv = typeof(UTF8Encoding).GetMethod("GetBytes", new[] { typeof(string), typeof(int), typeof(int), typeof(byte[]), typeof(int) });
            
            return Expression.IfThenElse(
                Expression.Equal(value, Expression.Constant(null, typeof(string))),
                WriteInt32(bufferParam, bufferSizeMethod, pos, Expression.Constant(0)),
                Expression.Block(
                    Expression.Assign(lenVar, Expression.Call(Expression.Constant(Encoding.UTF8), charCount, value)),
                    Expression.Call(bufferSizeMethod, bufferParam, pos, Expression.Add(Expression.Constant(4), lenVar)),
                    Expression.Assign(pos, WriteInt32(bufferParam, bufferSizeMethod, pos, Expression.Property(value, lenProp))),
                    Expression.AddAssign(pos, Expression.Call(Expression.Constant(Encoding.UTF8), strConv, value, Expression.Constant(0), Expression.Property(value, lenProp), bufferParam, pos)),
                    pos
                )
            );
        }

        private static Expression WriteInt16(Expression bufferParam, MethodInfo bufferSizeMethod, Expression pos, Expression value)
        {
            return Expression.Block(
                Expression.Call(bufferSizeMethod, bufferParam, pos, Expression.Constant(2)),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(value, typeof(byte))),
                Expression.Assign(Expression.ArrayAccess(bufferParam, Expression.PostIncrementAssign(pos)), Expression.Convert(Expression.RightShift(value, Expression.Constant(8)), typeof(byte))),
                pos
            );
        }        
        
    }
}
