复制到输出目录:
Action	说明	备注
Do not copy	不做数据同步	 
Copy always	每次都会被同步过去	 
Copy if newer	当有新版本存在时，才会被同步过去	 


生成操作:
Action	说明
None	资源既不会被集成到程序集内，也不会打包到xap包中。不过我们可以通过设置CopyToOutputDirectory选项让其自动拷贝到xap包所在目录。 这种情况下, 访问这个图片的相对Uri需要以"/"开始。 
适用场景：在大多数情况下，我们希望把video/audio文件放到xap的外面，因为这种文件一般都比较大，会影响silverlight应用的加载，而且一般的视频音频文件都是压缩格式的，放到xap中也不会起到减少他们文件大小的作用。 类似图片视频这种资源文件生成操作为None时和他们没有被添加到项目里是一样的，都可以用绝对Uri进行引用。
Compile	不适合用于资源文件。类文件要用"Compile"生成操作， 就是指项目里.cs或.vb文件。
Content	资源会被打包在Xap包里面。这种情况下, 访问这个图片的相对Uri需要以"/"开始。在这种方式下，如果没有在xap中找到图片文件，那么silverlight会自动从当前xap应用所在的文件夹下来找所需图片文件， 如果还没有找到那么就触发ImageFailed事件， 这种方式比较适合在多个程序集引用相同文件时采用。
Embedded Resource	这种方式会把文件嵌入到程序集中，Silverlight无法通过Uri引用在xaml和C#里对这个文件进行使用，微软不建议在Silverlight采用这种方式在程序集里嵌入资源。如果有这种需求可以用Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string path)相关的方法得到文件的Stream引用。
ApplicationDefinition	Silverlight程序的入口xaml文件(默认就是App.xaml)应该设置为这个"应用定义"。其他文件都不适合用这个。
Page	不适合用于资源文件。所有的用户控件，页面和子窗体(Usercontrol/Page/Childwindow)的xaml文件应该采用的生成操作。 如果改为别的方式那么会导致后台对应的代码文件无法链接到这个xaml文件。 采用"Page" build action时xaml里的错误会导致工程无法正确生成。
CodeAnalysisDictionary	代码分析使用，Silverlight中可以忽略
Resource	资源会被打包在程序集内部。 选择这种生成方式后,该资源文件会被嵌入到该应用的程序集中，就是说打开生成的xap是看不到这个文件的。 可以用相对于当前的XAML文件的相对Uri访问,<Image Source="sl.png" />或是<Image Source="./sl.png" />, 在子文件夹里的可以用<Image Source=”./images/ sl.png” />访问到。最保险的方式是采用特有的程序集资源URI访问,格式为 <Image Source="/{assemblyShortName};component/ sl.png "/>，这种方式还可以引用到xap中的其他程序集中的图片。这种生成方式的系统资源可以直接用Application.GetResourceStream(uri).Stream在代码里来得到。
SplashScreen	"SplashScreen"是这个选项是WPF的启动画面使用的。Silverlight启动加载画面是用的其他方式实现的，所以在Silverlight里不要用这个方式。
EntityDeploy	这个是EntityFramework采用的生成方式，在Silverlight里是没用。
