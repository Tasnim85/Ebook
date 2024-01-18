using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public class MenuFlyoutMenuItem
    {
        public MenuFlyoutMenuItem()
        {
            TargetType = typeof(MenuFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
        public string Icon { get; set; }
    }
}