using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ieProxy
{
    /// <summary>
    /// A TextBox control with built-in input validation.
    /// </summary>
    public partial class CheckedTextBox : TextBox, ISupportInitialize
    {
        #region Properties

        /// <summary>
        /// A delegate that returns whether a character is valid input. 
        /// Setting this allows you to customize AcceptedInput's filters.
        /// </summary>
        [Browsable(false)]
        public InputCheckDelegate AdditionalChecks
        {
            get { return _inputCheck; }
            set
            {
                _inputCheck = value;
                ValidateAllText();
            }
        }

        /// <summary>
        /// How to handle AdditionalChecks and AcceptedInput interop. 
        /// InputCheckMode.And is usually used to filter out existing accepted characters. 
        /// InputCheckMode.Or is usually used to add new accepted characters.
        /// </summary>
        [DefaultValue(InputCheckMode.And)]
        public InputCheckMode AdditionalCheckMode
        {
            get { return _delegateCheckMode; }
            set
            {
                _delegateCheckMode = value;
                ValidateAllText();
            }
        }

        /// <summary>
        /// What types of input are accepted by default by the textbox. 
        /// To extend this feature, add your own checks using AdditionalChecks.
        /// </summary>
        [DefaultValue(InputType.Normal)]
        public InputType AcceptedInput
        {
            get { return _acceptedInput; }
            set
            {
                _acceptedInput = value;
                ValidateAllText();
            }
        }

        /// <summary>
        /// The color to set the background to when invalid input has been detected.
        /// </summary>
        [DefaultValue(typeof(Color), "Red")]
        public Color InvalidBackColor
        {
            get { return _invalidBackColor; }
            set
            {
                _invalidBackColor = value;
                if (ContainsInvalidChars) BackColor = value;
            }
        }

        /// <summary>
        /// The normal color of the textbox when no invalid input was detected.
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        public Color NormalBackColor
        {
            get
            {
                return _normalBackColor;
            }
            set
            {
                _normalBackColor = value;
                if (!ContainsInvalidChars) BackColor = value;
            }
        }

        /// <summary>
        /// Gets whether the textbox contains invalid input.
        /// </summary>
        [Browsable(false)]
        public bool ContainsInvalidChars { get; private set; }

        /// <summary>
        /// Gets the integer value of the text in the textbox. 
        /// Only works with NumericOnly and NonNegativeNumericOnly input modes.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the accepted input mode does not support this.</exception>
        /// <exception cref="System.OverflowException">s represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
        [Browsable(false)]
        public int IntegerValue
        {
            get
            {
                switch (AcceptedInput)
                {
                    case InputType.Numeric:
                    case InputType.NonNegativeNumeric:
                        return int.Parse(Text);
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Gets the integer value of the text in the textbox. 
        /// Only works with Numeric(Only/WithDecimal) and 
        /// NonNegativeNumeric(Only/WithDecimal) input types.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the accepted input mode does not support this.</exception>
        /// <exception cref="System.OverflowException">s represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
        [Browsable(false)]
        public decimal DecimalValue
        {
            get
            {
                switch (AcceptedInput)
                {
                    case InputType.Numeric:
                    case InputType.NonNegativeNumeric:
                    case InputType.NumericWithDecimal:
                    case InputType.NonNegativeNumericWithDecimal:
                        return decimal.Parse(Text);
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        #endregion

        #region Fields

        //Set to true in case BeginInit() is not called. 
        //This is to improve performance by not validating 
        //input while the control is being created.
        private bool _created = true;

        private InputCheckDelegate _inputCheck;
        private InputCheckMode _delegateCheckMode = InputCheckMode.And;
        private InputType _acceptedInput = InputType.Normal;
        private Color _normalBackColor = Color.White;
        private Color _invalidBackColor = Color.Red;

        #endregion

        #region Types

        /// <summary>
        /// Represents a method that determines whether a given string 
        /// is valid input for the textbox.
        /// </summary>
        /// <param name="text">The string to validate.</param>
        public delegate bool InputCheckDelegate(string text);

        /// <summary>
        /// Specifies how AcceptedInput and AdditionalChecks interoperate to 
        /// determine whether the text is valid.
        /// </summary>
        public enum InputCheckMode
        {
            /// <summary>
            /// The text must meet the criteria of AcceptedInput AND AdditionalChecks.
            /// </summary>
            And,
            /// <summary>
            /// The text must meet the criteria of AcceptedInput OR AdditionalChecks.
            /// </summary>
            Or
        }

        /// <summary>
        /// Types of acceptable input. None means that all input will be rejected 
        /// unless AdditionalChecks is set and AdditionalCheckMode is set to Or.
        /// </summary>
        public enum InputType
        {
            /// <summary>
            /// Most input accepted (letters, numbers, and symbols)
            /// </summary>
            Normal,
            /// <summary>
            /// Letters (A-Z) and numbers (0-9)
            /// </summary>
            AlphaNumeric,
            /// <summary>
            /// Letters (A-Z)
            /// </summary>
            Alpha,
            /// <summary>
            /// Letters (A-Z) and space
            /// </summary>
            AlphaWithSpace,
            /// <summary>
            /// Numbers (0-9)
            /// </summary>
            Numeric,
            /// <summary>
            /// Numbers (0-9) and at maximum one decimal point (.)
            /// </summary>
            NumericWithDecimal,
            /// <summary>
            /// Numbers (0-9) and at maximum one negative sign (-)
            /// </summary>
            NonNegativeNumeric,
            /// <summary>
            /// Numbers (0-9), one negative sign (-), and one decimal point (.) only
            /// </summary>
            NonNegativeNumericWithDecimal,
            /// <summary>
            /// No input accepted. Use InputCheckMode.Or and AdditionalChecks to determine what is valid.
            /// </summary>
            None
        }

        #endregion

        #region Input checking

        /// <summary>
        /// Checks if the text contains normal input.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsNormal(string text)
        {
            return (text.All(c => (c >= 32 && c <= 126)));
        }

        /// <summary>
        /// Checks if the text contains only letters and numbers.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsAlphaNumeric(string text)
        {
            return (text.All(c => (c >= 'a' && c <= 'z')
                               || (c >= 'A' && c <= 'Z')
                               || (c >= '0' && c <= '9')));
        }

        /// <summary>
        /// Checks if the text contains only letters.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsAlphabetic(string text)
        {
            return (text.All(c => (c >= 'a' && c <= 'z')
                               || (c >= 'A' && c <= 'Z')));
        }

        /// <summary>
        /// Checks if the text contains only letters and spaces.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsAlphaWithSpace(string text)
        {
            return (text.All(c => (c >= 'a' && c <= 'z')
                               || (c >= 'A' && c <= 'Z')
                               || (c == ' ')));
        }

        /// <summary>
        /// Checks if the character is numeric (decimal point excluded).
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsNumeric(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c != '-')
                {
                    if (c < '0' || c > '9')
                    {
                        return false;
                    }
                }
                else
                {
                    if (i != 0) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the text contains numeric input and at maximum one decimal point.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsNumericWithDecimal(string text)
        {
            int decimalCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                switch (c)
                {
                    case '.':
                        if (++decimalCount == 2) return false;
                        break;
                    case '-':
                        if (i != 0) return false;
                        break;
                    default:
                        if (c < '0' || c > '9') return false;
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the text contains non-negative numeric input (decimals excluded).
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsNonNegativeNumeric(string text)
        {
            return (text.All(c => (c >= '0' && c <= '9')));
        }

        /// <summary>
        /// Checks if the text contains non-negative numeric input and at maximum one decimal point.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private static bool IsNonNegativeNumericWithDecimal(string text)
        {
            int decimalCount = 0;
            foreach (char c in text)
            {
                if (c == '.')
                {
                    if (++decimalCount == 2) return false;
                }
                else
                {
                    if (c < '0' || c > '9') return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether the text is valid input.
        /// </summary>
        /// <param name="text">The text to test.</param>
        private bool IsValidInput(string text)
        {
            bool normalChecksOk;

            switch (AcceptedInput)
            {
                case InputType.Normal:
                    normalChecksOk = IsNormal(text);
                    break;
                case InputType.AlphaNumeric:
                    normalChecksOk = IsAlphaNumeric(text);
                    break;
                case InputType.Alpha:
                    normalChecksOk = IsAlphabetic(text);
                    break;
                case InputType.AlphaWithSpace:
                    normalChecksOk = IsAlphaWithSpace(text);
                    break;
                case InputType.Numeric:
                    normalChecksOk = IsNumeric(text);
                    break;
                case InputType.NumericWithDecimal:
                    normalChecksOk = IsNumericWithDecimal(text);
                    break;
                case InputType.NonNegativeNumeric:
                    normalChecksOk = IsNonNegativeNumeric(text);
                    break;
                case InputType.NonNegativeNumericWithDecimal:
                    normalChecksOk = IsNonNegativeNumericWithDecimal(text);
                    break;
                default: //InputType.None
                    normalChecksOk = false;
                    break;
            }

            if (AdditionalChecks == null) return normalChecksOk;

            bool additionalChecksOk = AdditionalChecks(text);
            if (AdditionalCheckMode == InputCheckMode.Or) return normalChecksOk || additionalChecksOk;
            /*if (AdditionalCheckMode == InputCheckMode.And)*/ return normalChecksOk && additionalChecksOk;
        }

        /// <summary>
        /// Checks all characters in the textbox and sets input validity status.
        /// </summary>
        private void ValidateAllText()
        {
            if (!_created) return;

            if (!IsValidInput(Text))
            {
                SetInvalid();
                return;
            }

            SetValid();
        }

        /// <summary>
        /// Call this to set the background color to the invalid color (red).
        /// </summary>
        private void SetInvalid()
        {
            BackColor = _invalidBackColor;
            ContainsInvalidChars = true;
        }

        /// <summary>
        /// Call this to set the background color to the normal color (white).
        /// </summary>
        private void SetValid()
        {
            BackColor = _normalBackColor;
            ContainsInvalidChars = false;
        }

        #endregion

        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public void BeginInit()
        {
            _created = false;
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public void EndInit()
        {
            _created = true;
            ValidateAllText();
        }

        /// <summary>
        /// Raised when the text in the textbox has changed.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnTextChanged(EventArgs e)
        {
            ValidateAllText();

            base.OnTextChanged(e);
        }
    }
}