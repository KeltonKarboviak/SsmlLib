using Microsoft.VisualStudio.TestTools.UnitTesting;
using SsmlLib.Enums;
using System;

namespace SsmlLib.Tests.SsmlBuilderTests
{
    [TestClass]
    public class SsmlBuilderUnitTests
    {
        SsmlBuilder builder;

        [TestInitialize]
        public void InitializeEmptySsmlBuilderObject()
        {
            builder = new SsmlBuilder();
        }

        [TestCleanup]
        public void CleanupSsmlBuilderObject()
        {
            builder = null;
        }

        [TestMethod()]
        public void Say_WithNonEmptyString_AppendsStringToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "Hello there!");

            // act
            builder.Say("Hello there!");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML raw text not appended correctly.");
        }

        [TestMethod()]
        public void Say_WithEmptyString_AppendsEmptyStringToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                string.Empty);

            // act
            builder.Say(string.Empty);
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML raw text not appended correctly.");
        }

        [TestMethod()]
        public void Say_WithXmlSpecialChars_AppendsEscapedStringToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "3 is &lt; 5");

            // act
            builder.Say("3 is < 5");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML raw text not appended correctly.");
        }

        [TestMethod]
        public void Sentence_WithNonEmptyString_AppendsSentenceTagToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<s>Hello there!</s>");

            // act
            builder.Sentence("Hello there!");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <s> tag not appended correctly.");
        }

        [TestMethod]
        public void Sentence_WithEmptyString_AppendsEmptySentenceTagToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<s></s>");

            // act
            builder.Sentence(string.Empty);
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <s> tag not appended correctly.");
        }

        [TestMethod]
        public void BreakByDuration_WithValidDurationStringSeconds_AppendsBreakTagWithTimeAttributeToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<break time=\"5s\" />");

            // act
            builder.BreakByDuration("5s");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <break> tag not appended correctly.");
        }

        [TestMethod]
        public void BreakByDuration_WithInvalidDurationStringExceedsSeconds_ShouldThrowArgumentException()
        {
            // arrange

            // act
            try
            {
                builder.BreakByDuration("11s");
                var actual = builder.ToString();
            }
            catch (ArgumentException e)
            {
                // assert
                StringAssert.Contains(e.Message, SsmlBuilder.BreakDurationExceedsSecondsMessage);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void BreakByDuration_WithValidDurationStringMilliseconds_AppendsBreakTagWithTimeAttributeToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<break time=\"5000ms\" />");

            // act
            builder.BreakByDuration("5000ms");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <break> tag not appended correctly.");
        }

        [TestMethod]
        public void BreakByDuration_WithInvalidDurationStringExceedsMilliseconds_ShouldThrowArgumentException()
        {
            // arrange

            // act
            try
            {
                builder.BreakByDuration("11000ms");
                var actual = builder.ToString();
            }
            catch (ArgumentException e)
            {
                // assert
                StringAssert.Contains(e.Message, SsmlBuilder.BreakDurationExceedsMillisecondsMessage);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void BreakByDuration_WithInvalidDurationStringDoesNotMatchRegex_ShouldThrowArgumentException()
        {
            // arrange

            // act
            try
            {
                builder.BreakByDuration("5seconds");
                var actual = builder.ToString();
            }
            catch (ArgumentException e)
            {
                // assert
                StringAssert.Contains(e.Message, SsmlBuilder.BreakDurationDoesNotMatchRegexMessage);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void BreakByStrength_WithValidBreakStrength_AppendsBreakTagWithStrengthAttributeToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<break strength=\"medium\" />");

            // act
            builder.BreakByStrength(BreakStrengthOptions.Medium);
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <break> tag not appended correctly.");
        }

        [TestMethod]
        public void SayAs_WithNonEmptyStringAndValidInterpretAndDefaultFormat_AppendsSayAsTagWithInterpretAsAttributeToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<say-as interpret-as=\"cardinal\">3</say-as>");

            // act
            builder.SayAs("3", InterpretAsOptions.Cardinal);
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <say-as> tag not appended correctly.");
        }

        [TestMethod]
        public void SayAs_WithNonEmptyStringAndValidInterpretAndValidFormat_AppendsSayAsTagWithInterpretAsAndFormatAttributesToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<say-as interpret-as=\"date\" format=\"mdy\">07-25-2017</say-as>");

            // act
            builder.SayAs("07-25-2017", InterpretAsOptions.Date, "mdy");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <say-as> tag not appended correctly.");
        }

        [TestMethod]
        public void SayAs_WithEmptyStringAndValidInterpretAndDefaultFormat_AppendsSayAsTagWithEmptyStringAndInterpretAsAttributeToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<say-as interpret-as=\"cardinal\"></say-as>");

            // act
            builder.SayAs("", InterpretAsOptions.Cardinal);
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <say-as> tag not appended correctly.");
        }

        [TestMethod]
        public void SayAs_WithEmptyStringAndValidInterpretAndValidFormat_AppendsSayAsTagWithEmptyStringAndInterpretAsAndFormatAttributesToSpeakTag()
        {
            // arrange
            var expected = String.Format(
                "<speak version=\"{0}\" xmlns=\"{1}\" xml:lang=\"{2}\">{3}</speak>",
                SsmlBuilder.SsmlVersion,
                SsmlBuilder.SsmlNameSpace.NamespaceName,
                SsmlBuilder.SsmlLanguage,
                "<say-as interpret-as=\"date\" format=\"mdy\"></say-as>");

            // act
            builder.SayAs("", InterpretAsOptions.Date, "mdy");
            var actual = builder.ToString();

            // assert
            Assert.AreEqual(expected, actual, "SSML <say-as> tag not appended correctly.");
        }
    }
}
