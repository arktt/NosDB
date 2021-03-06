// /*
// * Copyright (c) 2016, Alachisoft. All Rights Reserved.
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// * http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */
using System;
using System.Runtime.Serialization;
using Alachisoft.NosDB.Serialization.IO;
using Alachisoft.NosDB.Common.Serialization;

namespace Alachisoft.NosDB.Serialization.Surrogates
{
    /// <summary>
    /// Surrogate for types that inherit from <see cref="ICompactSerializable"/>.
    /// </summary>
    class ICompactSerializableSerializationSurrogate : ContextSensitiveSerializationSurrogate
    {
        public ICompactSerializableSerializationSurrogate(Type t) : base(t) { }

        /// <summary>
        /// Non default object construction. The idea is to circumvent constructor calls
        /// and populate the object in <see cref="ICompactSerializable.Deserialize"/> method.
        /// </summary>
        /// <returns></returns>
        public override object Instantiate(CompactBinaryReader reader)
        {
            object obj = null;
            
            if(obj == null)
                obj = FormatterServices.GetUninitializedObject(ActualType);
            return obj;
        }

        public override object ReadDirect(CompactBinaryReader reader, object graph)
        {
            ((ICompactSerializable)graph).Deserialize(reader);
            return graph;
        }

        public override void WriteDirect(CompactBinaryWriter writer, object graph)
        {
            ((ICompactSerializable)graph).Serialize(writer);
        }

        public override void SkipDirect(CompactBinaryReader reader, object graph)
        {
            ((ICompactSerializable)graph).Deserialize(reader);
        }
    }
}