using SharedCommonViews;

namespace CustomerDtos
{
    public class CustomerView : ViewBase
    {
        public NameView Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
