using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using TinyUrlService.Services.Interfaces;

namespace TinyUrlService.Services
{
    public class AutomatedUserInput : IUserInput
    {
        private readonly Queue<string> _inputs;
        private readonly List<string> _outputs = new List<string>();
        private readonly Dictionary<string, string> _expectedResults;
        private readonly Action<string> _outputCallback;

        public AutomatedUserInput(IEnumerable<string> inputs, Dictionary<string, string> expectedResults, Action<string> outputCallback)
        {
            _inputs = new Queue<string>(inputs);
            _expectedResults = expectedResults;
            _outputCallback = outputCallback;
        }

        public string GetInput(string prompt)
        {
            _outputCallback(prompt);
            if (_inputs.Count > 0)
            {
                var input = _inputs.Dequeue();
                _outputCallback(input + Environment.NewLine);
                return input;
            }
            return string.Empty;
        }

        public void AddOutput(string output)
        {
            _outputs.Add(output);
        }

        public bool ValidateResults()
        {
            bool found = false;

            foreach (var expectedResult in _expectedResults)
            {
                foreach (var output in _outputs)
                {
                    if (output.Contains(expectedResult.Value))
                    {
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }
    }
}
