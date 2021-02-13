using InventoryManagementSoftware.Web;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Web
{
    public class Resource
    {
        private readonly IStringLocalizer _localizer;

        public Resource(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;
        }


        public string Action => _localizer[nameof(Action)];
        public string Name => _localizer[nameof(Name)];
        public string Attributes => _localizer[nameof(Attributes)];
        public string Attribute => _localizer[nameof(Attribute)];
        public string Brands => _localizer[nameof(Brands)];
        public string Brand => _localizer[nameof(Brand)];
        public string Add => _localizer[nameof(Add)];
        public string Edit => _localizer[nameof(Edit)];
        public string Delete => _localizer[nameof(Delete)];
        public string Categories => _localizer[nameof(Categories)];
        public string Category => _localizer[nameof(Category)];
        public string Save => _localizer[nameof(Save)];
        public string Close => _localizer[nameof(Close)];
        public string Product => _localizer[nameof(Product)];
        public string Products => _localizer[nameof(Products)];
        public string City => _localizer[nameof(City)];
        public string Cities => _localizer[nameof(Cities)];
        public string Address => _localizer[nameof(Address)];
        public string Addresses => _localizer[nameof(Addresses)];
        public string ErrNameIsRequired => _localizer[nameof(ErrNameIsRequired)];
        public string ErrBrandIsRequired => _localizer[nameof(ErrBrandIsRequired)];
        public string Description => _localizer[nameof(Description)];
        public string Price => _localizer[nameof(Price)];
        public string No => _localizer[nameof(No)];
        public string ChooseDepartment => _localizer[nameof(ChooseDepartment)];
        public string ChooseShelf => _localizer[nameof(ChooseShelf)];
        public string Details => _localizer[nameof(Details)];
        public string Value => _localizer[nameof(Value)];
        public string ChooseBrand => _localizer[nameof(ChooseBrand)];
        public string ChooseCategory => _localizer[nameof(ChooseCategory)];
        public string ChooseInventory => _localizer[nameof(ChooseInventory)];
        public string ChooseAttribute => _localizer[nameof(ChooseAttribute)];
        public string ForgotPassword => _localizer[nameof(ForgotPassword)];
        public string Email => _localizer[nameof(Email)];
        public string Login => _localizer[nameof(Login)];
        public string Password => _localizer[nameof(Password)];
        public string InventoryManagementSoftware => _localizer[nameof(InventoryManagementSoftware)];

    }
}
