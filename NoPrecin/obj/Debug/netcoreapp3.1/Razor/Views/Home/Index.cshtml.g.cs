#pragma checksum "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08209c348040e6fea14f66a502b1453118badebd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\_ViewImports.cshtml"
using NoPrecin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\_ViewImports.cshtml"
using NoPrecin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"08209c348040e6fea14f66a502b1453118badebd", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d25d3652d1214e01511b1d9979d2b8e9b6324a4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<NoPrecin.Models.Produtos>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""pricing-header px-3 py-3 pt-md-5 pb-md-4 mx-auto text-center"">
    <h1 class=""display-4"">No Precin Web</h1>
    <p class=""lead"">O nosso lema é pague pra ver! Nós garantimos que é tudo No Precin!</p>
    <p class=""lead"">Imagens meramente ilustrativas</p>
</div>
<div class=""album py-5 bg-light"">
    <div class=""container"">
        <div class=""row"">
");
#nullable restore
#line 14 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""col-md-4"">
                        <div class=""card mb-4 shadow-sm"">
                            <img class=""card-img-top"" data-src=""holder.js/100px225?theme=thumb&amp;bg=55595c&amp;fg=eceeef&amp;text=Thumbnail"" alt=""Thumbnail [100%x225]"" style=""height: 225px; width: 100%; display: block;"" src=""data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%22208%22%20height%3D%22225%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%20208%20225%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_173e09c5a93%20text%20%7B%20fill%3A%23eceeef%3Bfont-weight%3Abold%3Bfont-family%3AArial%2C%20Helvetica%2C%20Open%20Sans%2C%20sans-serif%2C%20monospace%3Bfont-size%3A11pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_173e09c5a93%22%3E%3Crect%20width%3D%22208%22%20height%3D%22225%22%20fill%3D%22%2355595c%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%2266.9453125%22%20y%3D%22117.3%22%3EThumbnail%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%");
            WriteLiteral("3E%3C%2Fsvg%3E\" data-holder-rendered=\"true\">\r\n                            <div class=\"card-body\">\r\n                                <p class=\"card-text\">\r\n                                    ");
#nullable restore
#line 21 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </p>\r\n                                <p class=\"card-text\">\r\n                                    ");
#nullable restore
#line 24 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Valor));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </p>
                                <div class=""d-flex justify-content-between align-items-center"">
                                    <div class=""btn-group"">
                                        <a  class=""btn btn-sm btn-outline-secondary""");
            BeginWriteAttribute("href", " href=\"", 2235, "\"", 2294, 1);
#nullable restore
#line 28 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml"
WriteAttributeValue("", 2242, Url.Action("Index", "Vendas", new { id = item.Id }), 2242, 52, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>Comprar</span></a>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 34 "C:\Users\TIAGO\Documents\GitHub\noprecinWEB\NoPrecin\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<NoPrecin.Models.Produtos>> Html { get; private set; }
    }
}
#pragma warning restore 1591
