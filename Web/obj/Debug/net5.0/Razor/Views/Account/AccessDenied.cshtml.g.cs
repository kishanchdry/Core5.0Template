#pragma checksum "F:\POC's\Admin\Web\Views\Account\AccessDenied.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4a006e6f988136fe17fa3687da5772593f542a54"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AccessDenied), @"mvc.1.0.view", @"/Views/Account/AccessDenied.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Data.Entities.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Services.IServices.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Services.IServices;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Shared.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Web.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "F:\POC's\Admin\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Hosting;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a006e6f988136fe17fa3687da5772593f542a54", @"/Views/Account/AccessDenied.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c61419c8fb5cd88a30266fc800672850c9a95789", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AccessDenied : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h1>Access denide please try with another user <a");
            BeginWriteAttribute("href", " href=\"", 49, "\"", 105, 1);
#nullable restore
#line 1 "F:\POC's\Admin\Web\Views\Account\AccessDenied.cshtml"
WriteAttributeValue("", 56, Url.Action("Login", "Account", new { area = ""}), 56, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Login Here</a></h1>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
