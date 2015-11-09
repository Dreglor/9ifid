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

using System;

namespace lib9ifid
{
    internal abstract class Command
    {
        protected readonly string Cmd;
        protected readonly object[] Parameters;
        
        protected Command(string commandName, object[] parameters, int expectedParameters = 0)
        {
            Cmd = commandName;
            Parameters = parameters;

            //sanity checks.
            if (string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentException("Command name must be set to a non-null non-empty value.", nameof(commandName));
            }

            if (parameters == null)
            {
                throw new ArgumentException("parameters must be set to a non-null value.", nameof(parameters));
            }

            if (Parameters.Length != expectedParameters)
            {
                throw new ArgumentException($"Number of arguments passed does not match what was expected. Expected: {expectedParameters}");
            }
        }

        public static Command BuildCommand(string arguments)
        {
            //transform ARMA arguments into objects that can be used.
            var passed = Utility.Deserialize(arguments);
            var commandName = passed[0][0] as string;
            var parameters = passed[1];

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(Parameters));
            }

            //build the correct command object based on the command name (command name are case-sensitive)
            switch (commandName)
            {
                case "KeyValueStore":
                    return new KeyValueStoreCommand(parameters);
                case "KeyValueLoad":
                    return new KeyValueLoadCommand(parameters);
                case "KeyValueRemove":
                    return new KeyValueRemoveCommand(parameters);
                default:
                    throw new Exception($"Command '{commandName}'not recognized");
            }

        }

        public abstract string Exec(); 
    }
}