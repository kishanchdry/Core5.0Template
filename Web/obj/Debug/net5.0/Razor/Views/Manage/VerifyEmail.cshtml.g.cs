#pragma checksum "F:\POC's\Admin\Web\Views\Manage\VerifyEmail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12b9756b2bcc67d9807aeccd82e561674410de1b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manage_VerifyEmail), @"mvc.1.0.view", @"/Views/Manage/VerifyEmail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12b9756b2bcc67d9807aeccd82e561674410de1b", @"/Views/Manage/VerifyEmail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c61419c8fb5cd88a30266fc800672850c9a95789", @"/Views/_ViewImports.cshtml")]
    public class Views_Manage_VerifyEmail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\POC's\Admin\Web\Views\Manage\VerifyEmail.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <span class=""col-md-12 Bold title"">Please Enter you otp: </span>
        <span class=""col-md-4 detail value""><input name=""OTP"" type=""text"" /></span>
        <span class=""col-md-8 detail value"">
            <span><input value=""verify"" class=""btn-verify"" type=""button""");
            BeginWriteAttribute("id", " id=\"", 369, "\"", 384, 1);
#nullable restore
#line 12 "F:\POC's\Admin\Web\Views\Manage\VerifyEmail.cshtml"
WriteAttributeValue("", 374, Model?.Id, 374, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></span>\r\n            <span><button class=\"resendOTP\" disabled");
            BeginWriteAttribute("id", " id=\"", 449, "\"", 464, 1);
#nullable restore
#line 13 "F:\POC's\Admin\Web\Views\Manage\VerifyEmail.cshtml"
WriteAttributeValue("", 454, Model?.Id, 454, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">Resend <span class=""timer"" max=""90"" data-text=""90""></span></button> </span>
        </span>
    </div>
</div>

<script>

    $(document).ready(function () {
        debugger;
        var timer = $("".timer"");
        
        var reverseTimerClock = setInterval(ReverseTimer, 1000);

        function ReverseTimer() {
            var revTimerVal = parseInt(timer.attr('data-text')) - 1;
            timer.text(""in "" + revTimerVal)
            timer.attr('data-text', revTimerVal)
            if (revTimerVal <= 0) {

                $("".resendOTP"").prop(""disabled"", false);

                clearInterval(reverseTimerClock);
            }
        }


        $(document).on(""click"", "".resendOTP"", function () {

            var _this = this;
            var Id = $(_this).attr(""id"");
            $.ajax({
                type: ""GET"",
                url: """);
#nullable restore
#line 45 "F:\POC's\Admin\Web\Views\Manage\VerifyEmail.cshtml"
                 Write(Url.Action("SendMailOTP", "Manage"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                data: {
                    ""id"": Id
                },
                success: function (response) {
                    timer.attr('data-text', 90)
                    $("".resendOTP"").prop(""disabled"", true);
                    reverseTimerClock = setInterval(ReverseTimer, 1000);
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });

        $(document).on(""click"", "".btn-verify"", function () {

            var _this = this;
            var Id = $(_this).attr(""id"");
            var otp = $(""[name='OTP']"").val();

            $.ajax({
                type: ""GET"",
                url: """);
#nullable restore
#line 71 "F:\POC's\Admin\Web\Views\Manage\VerifyEmail.cshtml"
                 Write(Url.Action("VerifyOTP", "Manage"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                data: {
                    ""id"": Id,
                    ""OTP"": otp,
                    ""isPhoneOTP"": false
                },
                success: function (response) {
                    $('#VerifyOTP').modal(""hide"");

                    swal(""Success"", ""Mail Verified Successfully"", ""success"");
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
    });

</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
    }
}
#pragma warning restore 1591