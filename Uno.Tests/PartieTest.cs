using System.Collections.Generic;
using Moq;
using Xunit;

namespace Uno.Tests
{
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;

        private Partie partie;

        public PartieTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();

            partie = new Partie(pileMock.Object, piocheMock.Object);
        }
    }
}