using System.Runtime.CompilerServices;
using AssetTracker.Services;
using AssetTracker.UI;

class Program
{
    static async Task Main(string[] args)
    {
        var manager = new AssetManager();
        var ui = new ConsoleUI(manager);
        await ui.RunAsync();
    }
}

