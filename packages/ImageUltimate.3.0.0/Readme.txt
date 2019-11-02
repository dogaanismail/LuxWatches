ImageUltimate v3.0.0.0
ASP.NET Image Resizer
Copyright © 2015-2018 GleamTech
https://www.gleamtech.com/imageultimate

Version 3.0.0 Release Notes:
https://docs.gleamtech.com/imageultimate/html/version-history.htm#v3.0.0

Online Documentation:
https://docs.gleamtech.com/imageultimate/

Support Portal:
https://support.gleamtech.com/

------------------------------------------------------------------------------------------------------
To use ImageUltimate in an ASP.NET MVC Project, do the following in Visual Studio:
------------------------------------------------------------------------------------------------------

1.  Set ImageUltimate's global configuration. For example, you may want to set the license key and 
    and the default path for finding source images (SourcePath). 
    Insert some of the following lines (if overriding a default value is required) into 
    the Application_Start method of your Global.asax.cs:

    ----------------------------------
    //Set this property only if you have a valid license key, otherwise do not
    //set it so ImageUltimate runs in trial mode.
    ImageUltimateConfiguration.Current.LicenseKey = "QQJDJLJP34...";

    //The default SourcePath value is "~/App_Data/ImageSource"
    //Both virtual and physical paths are allowed.
    //Note that using a path under "~/App_Data" can have a benefit,
    //for instance if you want to protect your original source files, i.e.
    //prevent them from being downloaded directly, you can use this special 
    //folder which is restricted by ASP.NET runtime by default.
    ImageUltimateWebConfiguration.Current.SourcePath = "~/Content";

    //The default CacheLocation value is "~/App_Data/ImageCache"
    //Both virtual and physical paths are allowed (or a Location instance for one of the supported 
    //file systems like Amazon S3 and Azure Blob).
    //However it's recommended that you use a path inside your application folder 
    //(e.g. "~/SomeFolder", "/SomeFolder" or "C:\inetpub\wwwroot\MySite\SomeFolder")
    //so that ImageUltimate can use RewritePath to pass download requests directly
    //to IIS for higher throughput.
    ImageUltimateWebConfiguration.Current.CacheLocation = "~/App_Data/ImageCache";
    ----------------------------------

    Alternatively you can specify the configuration in <appSettings> tag of your Web.config.

    ----------------------------------
    <add key="ImageUltimate:LicenseKey" value="QQJDJLJP34..." />
    <add key="ImageUltimateWeb:SourcePath" value="~/Content" />
    <add key="ImageUltimateWeb:CacheLocation" value="~/App_Data/ImageCache" />
    ----------------------------------

    As you would notice, ImageUltimate: prefix maps to ImageUltimateConfiguration.Current and 
    ImageUltimateWeb: prefix maps to ImageUltimateWebConfiguration.Current.      

2.  Open one of your View pages (eg. Index.cshtml) and at the top of
    your page add the necessary namespaces:

    ----------------------------------
    @using GleamTech.ImageUltimate
    @using GleamTech.ImageUltimate.AspNet
    @using GleamTech.ImageUltimate.AspNet.Mvc
    ----------------------------------

    Alternatively you can add the namespaces globally in Views/Web.config under 
    <system.web.webPages.razor>/<pages>/<namespaces> tag to avoid adding namespaces in your pages every time:

    ----------------------------------
    <add namespace="GleamTech.ImageUltimate" />
    <add namespace="GleamTech.ImageUltimate.AspNet" />
    <add namespace="GleamTech.ImageUltimate.AspNet.Mvc" />
    ----------------------------------

    Now in your page insert these lines:

    ----------------------------------
    @this.ImageTag("SomeImage.jpg", task => task.ResizeWidth(300))
    ----------------------------------

    This will resize width of the source image ~/Content/SomeImage.jpg to 300 pixels (keeping aspect ratio), 
    cache the resulting image and render a <img> tag in your page. For consecutive page views, 
    the image will be served directly from the cache and no processing will be done. 
    This is the reason the task (second parameter) is specified as a lambda function, it will be only 
    called when necessary for maximum performance. Note that as we specified a non-rooted path for 
    the image path (first parameter), the image is considered relative ,
    to ImageUltimateWebConfiguration.Current.SourcePath (as set in step 2). 
    This allows you to write image commands as short lines and avoids parent path repetition.
    
    Sometimes you may need to render a url instead of a <img> tag, then use:

    
    ----------------------------------  
    @this.ImageUrl("SomeImage.jpg", task => task.ResizeWidth(300))
    ----------------------------------  
    
    For example, consider you want to add a background image to a CSS class:

    ----------------------------------
    <style>
    .someCls
    {
        background-image: url(@this.ImageUrl("SomeImage.jpg", task => task.ResizeWidth(300)));
    }
    </style>
    ----------------------------------
    
------------------------------------------------------------------------------------------------------
To use ImageUltimate in an ASP.NET WebForms Project, do the following in Visual Studio:
------------------------------------------------------------------------------------------------------

1.  Set ImageUltimate's global configuration. For example, you may want to set the license key and 
    and the default path for finding source images (SourcePath). 
    Insert some of the following lines (if overriding a default value is required) into 
    the Application_Start method of your Global.asax.cs:

    ----------------------------------
    //Set this property only if you have a valid license key, otherwise do not
    //set it so ImageUltimate runs in trial mode.
    ImageUltimateConfiguration.Current.LicenseKey = "QQJDJLJP34...";

    //The default SourcePath value is "~/App_Data/ImageSource"
    //Both virtual and physical paths are allowed.
    //Note that using a path under "~/App_Data" can have a benefit,
    //for instance if you want to protect your original source files, i.e.
    //prevent them from being downloaded directly, you can use this special 
    //folder which is restricted by ASP.NET runtime by default.
    ImageUltimateWebConfiguration.Current.SourcePath = "~/Content";

    //The default CacheLocation value is "~/App_Data/ImageCache"
    //Both virtual and physical paths are allowed (or a Location instance for one of the supported 
    //file systems like Amazon S3 and Azure Blob).
    //However it's recommended that you use a path inside your application folder 
    //(e.g. "~/SomeFolder", "/SomeFolder" or "C:\inetpub\wwwroot\MySite\SomeFolder")
    //so that ImageUltimate can use RewritePath to pass download requests directly
    //to IIS for higher throughput.
    ImageUltimateWebConfiguration.Current.CacheLocation = "~/App_Data/ImageCache";
    ----------------------------------

    Alternatively you can specify the configuration in <appSettings> tag of your Web.config.

    ----------------------------------
    <add key="ImageUltimate:LicenseKey" value="QQJDJLJP34..." />
    <add key="ImageUltimateWeb:SourcePath" value="~/Content" />
    <add key="ImageUltimateWeb:CacheLocation" value="~/App_Data/ImageCache" />
    ----------------------------------

    As you would notice, ImageUltimate: prefix maps to ImageUltimateConfiguration.Current and 
    ImageUltimateWeb: prefix maps to ImageUltimateWebConfiguration.Current.      
      

2.  Open one of your pages (eg. Default.aspx) and at the top of your
    page add add the necessary namespaces:

    ----------------------------------
    <%@ Import Namespace="GleamTech.ImageUltimate" %>
    <%@ Import Namespace="GleamTech.ImageUltimate.AspNet" %>
    <%@ Import Namespace="GleamTech.ImageUltimate.AspNet.WebForms" %>
    ----------------------------------

    Alternatively you can add the namespaces globally in Web.config under 
    <system.web>/<pages>/<namespaces> tag to avoid adding namespaces in your pages every time:

    ----------------------------------
    <add namespace="GleamTech.ImageUltimate" />
    <add namespace="GleamTech.ImageUltimate.AspNet" />
    <add namespace="GleamTech.ImageUltimate.AspNet.WebForms" />
    ----------------------------------

    Now in your page insert these lines:

    ----------------------------------
    <%=this.ImageTag("SomeImage.jpg", task => task.ResizeWidth(300))%>
     ----------------------------------

    This will resize width of the source image ~/Content/SomeImage.jpg to 300 pixels (keeping aspect ratio), 
    cache the resulting image and render a <img> tag in your page. For consecutive page views, 
    the image will be served directly from the cache and no processing will be done. 
    This is the reason the task (second parameter) is specified as a lambda function, it will be only 
    called when necessary for maximum performance. Note that as we specified a non-rooted path for 
    the image path (first parameter), the image is considered relative ,
    to ImageUltimateWebConfiguration.Current.SourcePath (as set in step 2). 
    This allows you to write image commands as short lines and avoids parent path repetition.
    
    Sometimes you may need to render a url instead of a <img> tag, then use:

    
    ----------------------------------  
    <%=this.ImageUrl("SomeImage.jpg", task => task.ResizeWidth(300))%>
    ----------------------------------  
    
    For example, consider you want to add a background image to a CSS class:

    ----------------------------------
    <style>
        .someCls
        {
            background-image: url(<%=this.ImageUrl("SomeImage.jpg", task => task.ResizeWidth(300))%>);
        }
    </style>
    ----------------------------------

