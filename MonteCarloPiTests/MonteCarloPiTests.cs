using MonteCarloPi;
using Xunit;

namespace MonteCarloPiTests
{
    public class MonteCarloPiTests
    {
        [Fact]
        public void Arguments_parsed_correctly()
        {
            var input = new string[]{ "100" };

            Program.RunSimulation(input);
        }
    }
}
