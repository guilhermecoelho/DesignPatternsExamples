using Xunit;

namespace DesignPatternsExamples
{
    public class FactoryPattern
    {

    }
    public class Gender
    {
        public string DetermineGender(int input)
        {
            return DetermineGenderFactory.DetermineGender(input);
        }
    }

    public interface IDetermineGender
    {
        string GenderMale();
        string GenderFemale();
        string GenderUnknow();
    }

    public class DetermineGender : IDetermineGender
    {
        public string GenderFemale()
        {
            return "female";
        }

        public string GenderMale()
        {
            return "male";
        }

        public string GenderUnknow()
        {
            return "unknow";
        }
    }

    public class DetermineGenderFactory
    {
        public static string DetermineGender(int input)
        {
            if (input == 0) return new DetermineGender().GenderMale();
            if (input == 1) return new DetermineGender().GenderFemale();
            return new DetermineGender().GenderUnknow();
        }
    }

    public class TestFactoryPattern
    {
        [Theory]
        [InlineData(0, "male")]
        [InlineData(1, "female")]
        [InlineData(2, "unknow")]
        public void GenderTest_male(int input, string genderValue)
        {
            Gender gender = new Gender();

            var response = gender.DetermineGender(input);

            Assert.Equal(genderValue, response);
        }
    }
}
