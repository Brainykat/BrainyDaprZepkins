namespace SharedCommonViews
{
    public class NameView
    {
        public string Sur { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string FullName => $"{Sur} {First} {Middle}";
        public string FullNameReverse => $"{Middle} {Sur} {First}";
        public override string ToString() => $"{Sur} {First} {Middle}";
    }
}
