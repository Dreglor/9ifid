/*****************************************************************************\
|*
|* Copyright(c) 2015, Steven 'Dreglor' Garcia
|* All rights reserved.
|*
|* Redistribution and use in source and binary forms, with or without 
|* modification, are permitted provided that the following conditions are met:
|* 1. Redistributions of source code must retain the above copyright notice, 
|*		this list of conditions and the following disclaimer.
|* 2. Redistributions in binary form must reproduce the above copyright
|*		notice, this list of conditions and the following disclaimer in the
|*		documentation and / or other materials provided with the distribution.
|* 3. Neither the name of the copyright holder nor the names of its 
|*		contributors may be used to endorse or promote products derived from 
|*		this software without specific prior written permission.
|*
|* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
|* AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
|* IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
|* ARE DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
|* LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
|* CONSEQUENTIAL DAMAGES(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
|* SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
|* BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
|* WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT(INCLUDING NEGLIGENCE OR 
|* OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
|* ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
|*
\*****************************************************************************/

using System.IO;
using lib9ifid;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class KeyValueFeatureTest
    {
        [SetUp]
        public void Setup()
        {
            Extension.LastError = string.Empty;
            File.Delete("kv_test");
        }

        [Test]
        public void AddAndRetrieve()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueStore\"],[\"testing\",\"abc123\"]]"));
            Assert.AreEqual("abc123", Extension.Invoke("[[\"KeyValueLoad\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void RetrieveNonexistent()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueLoad\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void AddandRetrieveEmpty()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueStore\"],[\"testing\",\"\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueLoad\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void AddRemoveRetrieve()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueStore\"],[\"testing\",\"abc123\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueRemove\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueLoad\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void RemoveNonExistant()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueRemove\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void RemoveBadKey()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueRemove\"],[\"/\\\\!?|\"]]") );
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void AddBadKey()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueStore\"],[\"\\\\/!?|\", \"abc123\"]]"));
            Assert.AreNotEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void RetrieveBadKey()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueLoad\"],[\"\\\\/!?|\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }

        [Test]
        public void AddEmptyKey()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueStore\"],[\"\",\"abc123\"]]"));
            Assert.AreNotEqual(string.Empty, Extension.LastError);

        }

        [Test]
        public void AddEmptyValue()
        {
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueStore\"],[\"testing\",\"\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
            Assert.AreEqual(string.Empty, Extension.Invoke("[[\"KeyValueLoad\"],[\"testing\"]]"));
            Assert.AreEqual(string.Empty, Extension.LastError);
        }
    }
}
