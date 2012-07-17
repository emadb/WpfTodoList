using System.Windows.Media;
using XeDotNet.SimpleTodo.Converters;
using Xunit;

namespace XeDotNet.SimpleTodo.Fixtures.Converters
{
    public class CompletedToColorConverterFixture
    {
        [Fact]
        public void CompletedToColorConverter_False_ShouldReturnBlack()
        {
            CompletedToColorConverter converter = new CompletedToColorConverter();
            SolidColorBrush color = (SolidColorBrush)converter.Convert(false, typeof(SolidColorBrush), null, null);

            Assert.Equal(Colors.Black, color.Color);
        }

        [Fact]
        public void CompletedToColorConverter_True_ShouldReturnGray()
        {
            CompletedToColorConverter converter = new CompletedToColorConverter();
            SolidColorBrush color = (SolidColorBrush)converter.Convert(true, typeof(SolidColorBrush), null, null);

            Assert.Equal(Colors.LightGray, color.Color);
        }

    }
}