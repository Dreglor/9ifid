﻿/*****************************************************************************\
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

using System;
using System.IO;

namespace lib9ifid
{
    internal class KeyValueLoadCommand : Command
    {
        private readonly string _key;

        public KeyValueLoadCommand(object[] arguments) : base("KeyValueLoad", arguments, 1)
        {
            _key = Parameters[0] as string;

            if (_key == string.Empty)
            {
                throw new ArgumentException("key cannot be empty!");
            }
        }

        public override string Exec()
        {
            var filename = $"kv_{_key}";

            //return empty string if key has no value otherwise return value
            return !File.Exists(filename) ? string.Empty : File.ReadAllText(filename);
        }
    }
}