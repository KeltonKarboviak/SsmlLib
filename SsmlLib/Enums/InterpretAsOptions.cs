namespace SsmlLib.Enums
{
    /// <summary>
    /// Indicates the content type of text contained within an SSML say-as element.
    /// </summary>
    public enum InterpretAsOptions
    {
        None,

        /// <summary>
        /// The contained text is interpreted as an address.
        /// </summary>
        /// <example>
        /// <code>I'm at &lt;say-as interpret-as="address"&gt;150th CT NE, Redmond, WA&lt;/say-as&gt;</code>
        /// will be pronounced as "I'm at 150th court north east redmond washington".
        /// </example>
        Address,

        /// <summary>
        /// The contained text should be spoken as a cardinal number.
        /// </summary>
        /// <example>
        /// <code>There are &lt;say-as interpret-as="cardinal"&gt;3&lt;/say-as&gt; alternatives</code>
        /// will be pronounced as "There are three alternatives".
        /// </example>
        Cardinal,

        /// <summary>
        /// The contained text should be spoken as a cardinal number.
        /// </summary>
        /// <example>
        /// <code>There are &lt;say-as interpret-as="cardinal"&gt;3&lt;/say-as&gt; alternatives</code>
        /// will be pronounced as "There are three alternatives".
        /// </example>
        Number,

        /// <summary>
        /// Indicates that each letter in the contained text should be pronounced individually (spelled out).
        /// </summary>
        /// <example>
        /// <code>&lt;say-as interpret-as="characters"&gt;test&lt;/say-as&gt;</code>
        /// will be pronounced as "T E S T".
        /// </example>
        Characters,

        /// <summary>
        /// Indicates that each letter in the contained text should be pronounced individually (spelled out).
        /// </summary>
        /// <example>
        /// <code>&lt;say-as interpret-as="characters"&gt;test&lt;/say-as&gt;</code>
        /// will be pronounced as "T E S T".
        /// </example>
        SpellOut,

        /// <summary>
        /// The contained text is a date in the specified format. In the format designations, d=day, m=month, and y=year. The format for date indicates which date components are represented and their sequence.
        /// </summary>
        /// <example>
        /// <code>Today is &lt;say-as interpret-as="date" format="mdy"&gt;10-19-2016&lt;/say-as&gt;</code>
        /// will be pronounced as "Today is October nineteenth two thousand sixteen".
        /// </example>
        Date,

        /// <summary>
        /// The contained text should be interpreted as a sequence of individual digits.
        /// </summary>
        /// <example>
        /// <code>&lt;say-as interpret-as="number_digit"&gt;123456789&lt;/say-as&gt;</code>
        /// will be pronounced as "1 2 3 4 5 6 7 8 9"
        /// </example>
        Digits,

        /// <summary>
        /// The contained text should be interpreted as a sequence of individual digits.
        /// </summary>
        /// <example>
        /// <code>&lt;say-as interpret-as="number_digit"&gt;123456789&lt;/say-as&gt;</code>
        /// will be pronounced as "1 2 3 4 5 6 7 8 9"
        /// </example>
        NumberDigit,

        /// <summary>
        /// The contained text should be interpreted as a fractional number.
        /// </summary>
        /// <example>
        /// <code>&lt;say-as interpret-as="fraction"&gt;3/8&lt;/say-as&gt; of an inch</code>
        /// will be pronounced as "three eigths of an inch".
        /// </example>
        Fraction,

        /// <summary>
        /// The contained text should be interpreted as an ordinal number.
        /// </summary>
        /// <example>
        /// <code>Select the &lt;say-as interpret-as="ordinal"&gt;3rd&lt;/say-as&gt; option</code>
        /// will be pronounced as "Select the third option".
        /// </example>
        Ordinal,

        /// <summary>
        /// The contained text is a telephone number. The format attribute may contain digits that represent a country code, for example "1" for the United States or "39" for Italy. The speech synthesis engine may use this information to guide its pronunciation of a phone number. The country code may also be included in the phone number, and if so, takes precedence over the country code in the format attribute if there is a mismatch.
        /// </summary>
        /// <example>
        /// <code>The number is &lt;say-as interpret-as="telephone" format="1"&gt;(888) 555-1212&lt;/say-as&gt;</code>
        /// will be pronounced as "My number is area code eight eight eight five five five one two one two".
        /// </example>
        Telephone,

        /// <summary>
        /// The contained text is a time. Time may be expressed using a 12-hour clock (hms12), a 24-hour clock (hms24). Use a colon to separate numbers representing hours, minutes, and seconds. The following time strings are all valid examples: 12:35, 1:14:32, 08:15, and 02:50:45.
        /// </summary>
        /// <example>
        /// <code>The train departs at &lt;say-as interpret-as="time" format="hms12"&gt;4:00am&lt;/say-as&gt;</code>
        /// </example>
        Time
    }
}
