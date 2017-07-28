namespace SsmlLib
{
    using SsmlLib.Enums;
    using System;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    /// <summary>
    /// This class helps simplify creating SSML (Speech Synthesis Markup Language) using the builder pattern.
    /// This only supports a subset of SSML tags that Cortana devices support.
    /// </summary>
    /// <example>
    /// <code>
    /// var speech = new SSMLBuilder().Say("Hello, world!").BreakByDuration("1s").Say("My name is Joe.").ToString();
    /// </code>
    /// </example>
    public class SsmlBuilder
    {
        #region Public Const Fields
        public const string BreakDurationExceedsSecondsMessage = "The break duration exceeds the allowed 10 second duration.";
        public const string BreakDurationExceedsMillisecondsMessage = "The break duration exceeds the allowed 10,000 milliseconds duration.";
        public const string BreakDurationDoesNotMatchRegexMessage = "The duration must be a number followed by either 's' for second or 'ms' for milliseconds. e.g., 10s or 100ms. Max duration is 10 seconds (10000 milliseconds).";
        #endregion

        #region Public Static Fields
        public static readonly string SsmlVersion = "1.0";
        public static readonly XNamespace SsmlNameSpace = "https://www.w3.org/2001/10/synthesis";
        public static readonly string SsmlLanguage = "en-US";
        #endregion

        #region Private Static Fields
        /// <summary>
        /// Values to be used for the <c>BreakStrengthOptions</c> enum.
        /// </summary>
        private static readonly string[] BreakStrengthOptions = new string[]
        {
            "none",
            "x-weak",
            "weak",
            "medium",
            "strong",
            "x-strong"
        };

        /// <summary>
        /// Values to be used for the <c>InterpretAsOptions</c> enum.
        /// </summary>
        private static readonly string[] InterpretAsOptions = new string[]
        {
            "none",
            "address",
            "cardinal",
            "number",
            "characters",
            "spell-out",
            "date",
            "digits",
            "number_digit",
            "fraction",
            "ordinal",
            "telephone",
            "time"
        };
        #endregion

        #region Private Fields
        private XElement _root;
        #endregion

        #region Public C'tors
        /// <summary>
        /// Initializes a new instance of the <see cref="SsmlBuilder"/> class.
        /// </summary>
        public SsmlBuilder()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SsmlBuilder"/> class.
        /// </summary>
        /// <param name="text">The raw text to insert into the speak tag.</param>
        public SsmlBuilder(string text)
        {
            this._root = new XElement(
                SsmlNameSpace + "speak",
                new XAttribute("version", SsmlVersion),
                new XAttribute("xmlns", SsmlNameSpace.NamespaceName),
                new XAttribute(XNamespace.Xml + "lang", SsmlLanguage),
                text.Trim());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This appends raw text into the &lt;speak&gt; tag.
        /// </summary>
        /// <param name="text">The raw text to insert into the speak tag.</param>
        /// <returns>Returns `this` SSMLBuilder instance.</returns>
        public SsmlBuilder Say(string text)
        {
            this._root.Add(new XText(
                text.Trim()));

            return this;
        }

        /// <summary>
        /// Creates and appends a &lt;s&gt; tag into the &lt;speak&gt; tag.
        /// <para>
        /// See https://docs.microsoft.com/en-us/cortana/reference/ssml#p-and-s-elements
        /// </para>
        /// </summary>
        /// <param name="text">The raw sentence text to insert into the speak tag.</param>
        /// <returns>Returns `this` SSMLBuilder instance.</returns>
        public SsmlBuilder Sentence(string text)
        {
            this._root.Add(new XElement(
                SsmlNameSpace + "s",
                text.Trim()));

            return this;
        }

        /// <summary>
        /// Creates and appends a &lt;break&gt; tag into the &lt;speak&gt; tag that will pause the audio based upon the duration provided. This method will also ensure the break duration conforms to the duration restrictions.
        /// <para>
        /// See https://docs.microsoft.com/en-us/cortana/reference/ssml#break-element
        /// </para>
        /// </summary>
        /// <param name="duration">The duration represented by a number + either 's' for second or 'ms' for milliseconds.</param>
        /// <returns>Returns `this` SSMLBuilder instance.</returns>
        public SsmlBuilder BreakByDuration(string duration)
        {
            Regex re = new Regex(@"^(\d*\.?\d+)(s|ms)$");
            Match m = re.Match(duration);

            if (m.Success)
            {
                var breakDuration = m.Groups[1].Value;
                var breakUnit = m.Groups[2].Value;

                if (breakUnit.ToLower().Equals("s") && Convert.ToInt32(breakDuration) > 10)
                {
                    throw new ArgumentException(BreakDurationExceedsSecondsMessage, "duration");
                }
                else if (Convert.ToInt32(breakDuration) > 10000)
                {
                    throw new ArgumentException(BreakDurationExceedsMillisecondsMessage, "duration");
                }
            }
            else
            {
                throw new ArgumentException(BreakDurationDoesNotMatchRegexMessage, "duration");
            }

            this._root.Add(new XElement(
                SsmlNameSpace + "break",
                new XAttribute("time", duration)));

            return this;
        }

        /// <summary>
        /// Creates and appends a &lt;break&gt; tag into the &lt;speak&gt; tag that will pause the audio based upon the strength provided.
        /// <para>
        /// See https://docs.microsoft.com/en-us/cortana/reference/ssml#break-element
        /// </para>
        /// </summary>
        /// <param name="strength">The strength represented as a value from the BreakStrengthOptions enum.</param>
        /// <returns>Returns `this` SSMLBuilder instance.</returns>
        public SsmlBuilder BreakByStrength(BreakStrengthOptions strength)
        {
            var s = BreakStrengthOptions[(int)strength];

            this._root.Add(new XElement(
                SsmlNameSpace + "break",
                new XAttribute("strength", s)));

            return this;
        }

        /// <summary>
        /// Creates and appends a &lt;say-as&gt; tag into the &lt;speak&gt; tag that has multiple attributes such as interpret-as and format.
        /// <para>
        /// See https://docs.microsoft.com/en-us/cortana/reference/ssml#say-as-element
        /// </para>
        /// </summary>
        /// <param name="text">The raw text to be interpreted.</param>
        /// <param name="interpret">The interpret-as attribute that indicates the content type of text contained in the element.</param>
        /// <param name="format">The format attribute that provides additional information about the precise formatting of the text contained in the element.</param>
        /// <returns>Returns `this` SSMLBuilder instance.</returns>
        public SsmlBuilder SayAs(string text, InterpretAsOptions interpret, string format = "")
        {
            var interpretOption = InterpretAsOptions[(int)interpret];

            var sayAsElement = new XElement(
                SsmlNameSpace + "say-as",
                new XAttribute("interpret-as", interpretOption),
                text.Trim());

            if (!string.IsNullOrEmpty(format))
            {
                sayAsElement.SetAttributeValue("format", format);
            }

            this._root.Add(sayAsElement);

            return this;
        }

        /// <summary>
        /// Creates and returns an equivalent SSML string.
        /// </summary>
        /// <returns>An SSML string.</returns>
        public override string ToString()
        {
            // Use SaveOptions.DisableFormatting so that it doesn't add unnecessary newlines
            return this._root.ToString(SaveOptions.DisableFormatting);
        }
        #endregion
    }
}