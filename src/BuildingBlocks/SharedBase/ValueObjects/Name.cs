namespace SharedBase.ValueObjects
{
    public class Name
    {
        //Name Should not really be a value object as per my thoughts coz its not necessarily immutable
        public Name() { }
        public static Name Create(string sur, string first, string middle = null)
        {
            return new Name(sur, first, middle);
        }
        private Name(string sur, string first, string middle = null)
        {
            if (string.IsNullOrWhiteSpace(sur) || string.IsNullOrWhiteSpace(first)) throw new ArgumentNullException(nameof(FullName));
            Sur = sur.Trim(); First = first.Trim();
            Middle = middle != null ? middle.Trim() : null;
        }
        public string Sur { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string FullName => $"{Sur} {First} {Middle}";
        public string FullNameReverse => $"{Middle} {Sur} {First}";
        public override string ToString() => $"{Sur} {First} {Middle}";
    }
}
