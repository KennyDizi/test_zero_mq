using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReliablePubSub.Client;
using ReliablePubSub.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace TestZeroMQ
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

	    protected override void OnStart()
	    {
	        // Handle when your app starts
	        /*var client = new ReliableClient(new[] {"tcp://192.168.1.6:6669"}, TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(10),
                m =>
                {
                    Console.WriteLine(
                        $"Message received. Topic: {m.First.ConvertToString()}, Message: {m.Last.ConvertToString()}");
                }, (x, m) => Console.WriteLine($"Error in message handler. Exception {x} Message {m}"),
                welcomeMessageHandler:
                () => { Debug.WriteLine("==========================Well come================"); });

            client.Subscribe("trungdc");*/

	        var knownTypes = new Dictionary<Type, TypeConfig>
	        {
	            {
	                typeof(MyMessage), new TypeConfig
	                {
	                    Serializer = new WireSerializer(),
	                    Comparer = new DefaultComparer<MyMessage>(),
	                    KeyExtractor = new DefaultKeyExtractor<MyMessage>(x => x.Key)
	                }
	            }
	        };

	        var topics = new Dictionary<string, Type> {{"trungdc", typeof(MyMessage)}};

	        var cache = new DefaultLastValueCache<object>(topics.Keys,
	            (topic, key, value) =>
	            {
	                Console.WriteLine(
	                    $"Client Cache Updated. Topic:{topic} Key:{key} Value:{value} ClientTime:{DateTime.Now:hh:mm:ss.fff}");
	            });

	        /*using (new Subscriber(new[] { "tcp://192.168.1.6" }, 6669, 6668, knownTypes, topics, cache))
	        {
	            while (true)
	            {
	            }
	        }*/

	        var subscripber = new Subscriber(new[] {"tcp://192.168.1.6"}, 6669, 6668, knownTypes, topics, cache);
	        subscripber.StartSocket();
	    }

	    protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
