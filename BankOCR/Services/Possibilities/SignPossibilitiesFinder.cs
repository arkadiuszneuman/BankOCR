using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankOCR.Services.Possibilities
{
    public interface ISignPossibilitiesFinder
    {
        IEnumerable<char> FindOtherPossibilitiesOfASign(string digitalSign);
    }

    public class SignPossibilitiesFinder : ISignPossibilitiesFinder
    {
        private readonly IDigitParserService _digitParserService;
        private static readonly List<PipeAndDashPossibility> _pipeAndDashPossibilities = new();

        static SignPossibilitiesFinder()
        {
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(0, 1, '_'));
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(1, 0, '|'));
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(1, 1, '_'));
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(1, 2, '|'));
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(2, 0, '|'));
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(2, 1, '_'));
            _pipeAndDashPossibilities.Add(new PipeAndDashPossibility(2, 2, '|'));
        }

        public SignPossibilitiesFinder(IDigitParserService digitParserService)
        {
            _digitParserService = digitParserService;
        }

        public IEnumerable<char> FindOtherPossibilitiesOfASign(string digitalSign)
        {
            var splittedByLine = SplitByNewLine(digitalSign).ToArray();
            return FindPossibilities(splittedByLine);
        }

        private IEnumerable<char> FindPossibilities(string[] splittedByLine)
        {
            foreach (var pipeAndDashPossibility in _pipeAndDashPossibilities)
            {
                var newArray = new string[splittedByLine.Length];
                splittedByLine.CopyTo(newArray, 0);

                var arrayValue = newArray[pipeAndDashPossibility.Y][pipeAndDashPossibility.X];
                var sb = new StringBuilder(newArray[pipeAndDashPossibility.Y]);

                if (arrayValue == pipeAndDashPossibility.Sign)
                    sb[pipeAndDashPossibility.X] = ' ';
                else if (arrayValue == ' ') 
                    sb[pipeAndDashPossibility.X] = pipeAndDashPossibility.Sign;

                newArray[pipeAndDashPossibility.Y] = sb.ToString();
                
                var newDigit = _digitParserService.ParseDigit(newArray[0] + newArray[1] + newArray[2]);
                if (newDigit != null)
                    yield return newDigit.ToString()[0];
            }
        }

        private IEnumerable<string> SplitByNewLine(string digitalSign)
        {
            for (var x = 0; x <= 6; x += 3)
                yield return string.Concat(digitalSign.Skip(x).Take(3));
        }
        
        private class PipeAndDashPossibility
        {
            public int X { get; }
            public int Y { get; }
            public char Sign { get; }

            public PipeAndDashPossibility(int y, int x, char sign)
            {
                Y = y;
                X = x;
                Sign = sign;
            }
        }
    }
}