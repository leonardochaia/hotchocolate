using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrawberryShake.CodeGeneration.CSharp.Builders
{
    public class LambdaBuilder : ICode
    {
        private readonly List<string> _arguments = new();
        private ICode? _code;

        public LambdaBuilder AddArgument(string value)
        {
            _arguments.Add(value);
            return this;
        }

        public LambdaBuilder SetCode(ICode code)
        {
            _code = code;
            return this;
        }

        public void Build(CodeWriter writer)
        {
            if (_code is null)
            {
                throw new ArgumentNullException(nameof(_code));
            }

            if (_arguments.Count > 1)
            {
                writer.Write('(');
            }

            for (var i = 0; i < _arguments.Count; i++)
            {
                if (i > 0)
                {
                    writer.Write(',');
                }

                writer.Write(_arguments[i]);
            }

            if (_arguments.Count > 1)
            {
                writer.Write(')');
            }

            writer.Write(" => ");
            _code.Build(writer);
        }

        public static LambdaBuilder New() => new();
    }
}