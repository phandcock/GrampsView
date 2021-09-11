namespace GrampsView.Common.CustomClasses
{
    using System;

    public struct FormattedChar
    {
        public char Character
        {
            get; set;
        }

        public bool StyleBold
        {
            get; set;
        }

        public bool StyleItalics
        {
            get; set;
        }

        public bool StyleSuperscript
        {
            get; set;
        }

        public bool StyleUnderline
        {
            get; set;
        }

        public static bool operator !=(FormattedChar lhs, FormattedChar rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator ==(FormattedChar lhs, FormattedChar rhs)
        {
            return lhs.Equals(rhs);
        }

        public override bool Equals(Object obj)
        {
            return obj is FormattedChar && Equals((FormattedChar)obj);
        }

        public bool Equals(FormattedChar other)
        {
            return (StyleBold == other.StyleBold) && (StyleItalics == other.StyleItalics);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}