# From Scripting to Framework course on TestAutomationU.com

## Chapter 1 - Machine Setup

1. Requirements
    - .NET Core version 2.2 (latest at time of recording) or greater
    - VS Code

2. Make a new project called `scripting-to-framework` and open it in VS Code

3. Install Extensions in VS Code
    - C# by Microsoft
    - PackSharp by Carlos Kidman

4. Open the Command Palette for each of these commands
    - `PackSharp: Create New Project` > select `Class Library` > call the Project "Framework"
    - `PackSharp: Create New Project` > select `Class Library` > call the Project "Royale"
    - `PackSharp: Create New Project` > select `NUnit 3 Test Project` > call the Project "Royale.Tests"
    - `PackSharp: Create New Project` > select `Solution File` > call the Project "StatsRoyale"

> NOTE: The Solution (.sln) file will manage the Project (.csproj) files while the Project files handle their own packages and dependencies. As you add things, this is all handled for you! Very cool.

5. Add the Projects to the Solution. Run these commands in the Terminal:
    - `$ dotnet sln add Framework`
    - `$ dotnet sln add Royale`
    - `$ dotnet sln add Royale.Tests`

6. Build the Solution so we know everything is working
    - `$ dotnet build`

7. Open the `UnitTest1.cs` file in the Royale.Tests project
    - C# will "spin up" so you can start coding in C# and get helpful completions, hints, etc.
    - VS Code will ask if you want "Required assets to build and debug". Add them by clicking the "Yes" button.
        - If you do not get this immediately, try closing VS Code and reopening the project.
        - This will add a `.vscode` folder to your solution, but this is required to run and debug the tests.

8. Run the Tests
    - `$ dotnet test`
    - This will run all the tests, but you only have one right now. It should pass.


## Chapter 2 - Script Some Tests

> NOTE: Our application under test (website) is https://statsroyale.com

1. Change the name of the Test Class and Test Method so they make more sense
    - Test Class from `Tests` to `CardTests`
    - File name from `Tests.cs` to `CardTests.cs`
    - Test Method from `Test1()` to `Ice_Spirit_is_on_Cards_Page()`

2. Install Selenium NuGet (package) with PackSharp
    - Open Command Palette > select `PackSharp: Bootstrap Selenium` > add to `Royale.Tests`, our Test Project
    - If you open the `Royale.Tests.csproj` file, you will see that Selenium packages have been added
    - You will also see a `_drivers` directory is added at the Workspace root

> NOTE: This command installs the **latest** versions of chromedriver and the Selenium packages.

3. Use Selenium in our CardTests.cs file
    - Within the CardTests class, add the `IWebDriver driver;` field to the top
    - Resolve the error by hovering the red line and click on the lightbulb
        - The first option will want you to add the `using OpenQA.Selenium;` statement. Select that one
        - The error will go away and you will see that using statement is added automatically

4. SetUp and TearDown methods
    - The [SetUp] method is run *before each* test. Change the method name from `Setup()` to `BeforeEach()`
    - Add a [TearDown] method which runs *after each* test. Call this method `AfterEach()`

5. Within AfterEach(), add:
    - `driver.Quit();`
    - This will close the driver after each test is finished

6. Within BeforeEach(), add:
    - `driver = new ChromeDriver(<path-to-chromedriver>);`
    - This will open a new Chrome browser for every test
    - "Point" the ChromeDriver() to wherever you store your `chromedriver` or `chromedriver.exe`.

> NOTE: Everyone manages their drivers (like `chromedriver`, `geckodriver`, etc.) differently. Use your preferred method.

7. Write the first test. The steps are:
    1. Go to https://statsroyale.com
    2. Click on Cards link
    3. Assert Ice Spirit is displayed

8. For the second test, the steps are:
    1. Go to https://statsroyale.com
    2. Click on Cards link
    3. Click on Ice Spirit card
    4. Assert the basic headers are correct. These headers are:
        - Name ("Ice Spirit")
        - Type ("Troop")
        - Arena ("Arena 8")
        - Rarity ("Common")

9. There's a lot of code in this one, so make sure to pause and replay as much as you need :)


## Chapter 3 - Page Object Model

Follow the video to for an explanation on the `Page Object Model` and `Page Map Pattern`.

1. Within the Royale project, create a `Pages` directory. This is where all of our Page objects will live.

2. Move the `Class1.cs` file into `Pages` and rename it to `HeaderNav.cs`

3. Within the file, rename `Class1` to `HeaderNav` and then make another class called `HeaderNavMap`

4. Use PackSharp to restructure our packages and dependencies so we leverage Framework and Royale projects
    1. Move Selenium to the `Framework` project
        - Open Command Palette > `PackSharp: Bootstrap Selenium` > select `Framework`
    2. Remove Selenium from `Royale.Tests` project
        - Open Command Palette > `PackSharp: Remove Package` > select `Royale.Tests` > select `Selenium.Support`
        - Also remove `Selenium.WebDriver`

5. Framework is our base, so we want the projects to reference each other in a linear way.

    Framework -> Royale -> Royale.Tests

    `Royale.Tests` will reference `Royale` which references `Framework`

    - Open Command Palette > `PackSharp: Add Project Reference` > select `Royale.Tests` > select `Royale`
    - Open Command Palette > `PackSharp: Add Project Reference` > select `Royale` > select `Framework`

6. Now we can bring in `using OpenQA.Selenium` in our `HeaderNav.cs` file

> NOTE: The rest of this video is very "code-heavy", so make sure to follow along there

7. The naming convention for Pages and Page Maps is very simple. If you have a Home page, then you would do this:
    - Page => `HomePage`
    - Map => `HomePageMap`

8. In the video, there is a "jump" from `11:22` to `11:25` where I am able to use the `Map.Card()` immediately. At this point, you will have an error. All you need to do is:
    - Add the `public readonly CardPageMap Map;` field at the top of the `CardsPage` class and the Constructor as well
    - You will see the needed code at `11:43`. Sorry about that!

9. Card Details Page and Map
    - Take a moment to pause the video and copy the code to move forward

10. At the end of the video, run your second test. It should fail! Your challenge is to solve this error so the test passes.


## Chapter 4 - Models and Services

1. Copy and Paste the second test to make a third test. However, this test will be for the "Mirror" card, so change the values in the test accordingly.

2. In the Framework project, make a new folder called `Models` and move the `Class1.cs` file into it.

3. Rename the file to `MirrorCard.cs` and change the class name too

4. We'll be adding the card properties that we care about and give them default values.

> NOTE: Some portions are sped up, so just pause the video when it gets back to regular speed and copy as needed.

5. Because `MirrorCard` and `IceSpiritCard` share the same properties, we can create a "base" `Card` with these properties and have our cards inherit it. This is very similar to how our `PageBase` class works. Every page on the website has access to the `HeaderNav`, right? Instead of repeating the header navigation bar on every page, we can create it once and then share it to every page! This helps us avoid repeating ourselves as well as simplifying our code.

    - Copy and Paste the `MirrorCard` into a new file called `IceSpiritCard.cs` in Models.
    - Change the default values to the Ice Spirit values

6. Now we can bring this all together by creating a `GetBaseCard()` method in our `CardDetailsPage`. Pause the video as needed.

7. We can use this method in the Mirror test by getting two cards:
    - `var card = cardDetails.GetBaseCard();`
        - This card is the one we get off of the page using Selenium and our Page objects
    - `var mirror = new MirrorCard();`
        - This card has the actual values that we expect our Mirror Card to have

8. Assert that the expected values from our `var mirror` card match the actual values we received from `var card`.

9. Our first Card Services

    These "services" are abstractions of how we end up getting cards from a data store.

    The "In Memory" card service will help with getting cards from local, hard-coded values we've specified in our Models directory, but ultimately we'd like to get these values from actual data stores like a database. We will be doing that in a future chapter :)

10. Now that the Services are complete and being used in the test, our 2nd and 3rd tests are almost identical. The only difference are the names!

    - We can now leverage the `[TestCase]` and `[TestSource]` attributes from the `NUnit Test Framework`
    - These will "feed" values into the test
    - This turns a single Test Method into multiple Test Cases!

11. We can use the `[Parallelizable]` attribute to make these Test Cases run in parallel!
    - `[Parallelizable(ParallelScope.Children)]`

12. Run the 3rd test. It will spin up two browsers at the same time, but most likely both with fail. What happened?
    - You will also notice that one of the two browsers doesn't close even after failing. Can you guess why?
    - The answer is that we are instantiating a single WebDriver for both tests, so they are fighting each other over the driver!
    - We will solve this in the next chapter

13. Last thing we'll do is add the `[Category]` attribute to our test
    - `[Category("cards")]`
    - We can use Categories when running our tests. To run only the tests with the Category of "cards", we would do:
        - `$ dotnet test --filter testcategory=cards`


## Chapter 5 - Customizing WebDriver

1. Start by creating a `Selenium` folder in our `Framework` project

2. Create a `Driver.cs` file in `Selenium`. This is where our wrapper of WebDriver will exist.

3. The key to achieving the simplicity of a `static` even though it's not a singleton is with the `[ThreadStatic]` attribute.
    ```c#
    [ThreadStatic]
    private static IWebDriver _driver;

    public static void Init()
    {
        _driver = new ChromeDriver();
    }

    public static IWebDriver Current => _driver ?? throw new System.ArgumentException("_driver is null.");
    ```

    - This is the bread and butter of this approach. `_driver` is the instance of WebDriver on a thread.
    - `Current` is how you access the current instance of WebDriver for the test you're on

4. In the `BeforeEach()` method of CardTests, you can now use `Driver.Init();` and remove the "global" `IWebDriver driver` at the top.

5. Then create a "Pages Wrapper"
    1. Create `Pages.cs` file in `Royale.Pages` directory
    2. Create a field for each page we have
        ```c#
        [ThreadStatic]
        public static CardsPage Cards;

        [ThreadStatic]
        public static CardDetailsPage CardDetails;

        public static void Init()
        {
            Cards = new CardsPage(Driver.Current);
            CardDetails = new CardDetailsPage(Driver.Current);
        }
        ```

6. In the tests, we can get rid of any lines that say `new Page()` because our Pages Wrapper handles that for us
    - Replace: `var cardsPage = new CardsPage(Driver.Current);`
    - With: `Pages.Cards`

7. In our `Driver` class, let's add a way to navigate to a URL so we don't have to say `Driver.Current.Url`
    ```c#
    public static void Goto(string url)
    {
        if (!url.StartsWith("http"))
        {
            url = $"http://{url}";
        }

        Debug.WriteLine(url);
        Current.Navigate().GoToUrl(url);
    }
    ```

8. Your `BeforeEach()` method should now look like this:
    ```c#
    [SetUp]
    public void BeforeEach()
    {
        Driver.Init();
        Pages.Init();
        Driver.Goto("https://statsroyale.com");
    }
    ```

9. Now add a `FindElement()` and `FindElements()` method to Driver.

10. Go to each of the Page Objects and replace `_driver` with `Driver`.
    - We no longer need the `IWebDriver _driver` field in the Page Maps
    - We no longer need the constructors in the Page Maps
    - We no longer need to pass in `IWebDriver driver` in Page Object constructors!

11. Yes, a lot of code is deleted, but that's a good thing!

12. The challenge is to add a `Quit()` method to our Driver class to get rid of `Driver.Current.Quit();` in the `AfterEach()`.


## Chapter 6 - Test Data from an API

1. From Postman, we saw the the /cards endpoint returned all of the cards that exist in the game. However, they had a different shape than our Card class. Update our Card class to include these new properties:

    ```c#
    public class Card
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Icon { get; set; }

        public virtual int Cost { get; set; }

        public virtual string Rarity { get; set; }

        public virtual string Type { get; set; }

        public virtual string Arena { get; set; }
    }
    ```

2. We want to leverage that endpoint by creating an ApiCardService
    - Create `ApiCardService.cs` within `Framework.Services`
    - Implement the `ICardService` interface

3. Install the `Newtonsoft.Json` package to the `Framework` project
    - Open Command Palette > `PackSharp: Add Package` > search `Newtonsoft` > select `Newtonsoft.Json`

4. Install the `RestSharp` package to the `Framework` project
    - Open Command Palette > `PackSharp: Add Package` > search `RestSharp` > select `RestSharp`

5. Within the `ApiCardService` class, we will use RestSharp to make the call just like PostMan. We'll call this method `GetAllCards()`

    ```c#
    public const string CARDS_API = "https://statsroyale.com/api/cards";

    public IList<Card> GetAllCards()
    {
        var client = new RestClient(CARDS_API);
        var request = new RestRequest
        {
            Method = Method.GET,
            RequestFormat = DataFormat.Json
        };

        var response = client.Execute(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new System.Exception("/cards endpoint failed with " + response.StatusCode);
        }

        return JsonConvert.DeserializeObject<IList<Card>>(response.Content);
    }
    ```

6. Now let's implement the `GetCardByName(string cardName)` method

    ```c#
    public Card GetCardByName(string cardName)
    {
        var cards = GetAllCards();
        return cards.FirstOrDefault(card => card.Name == cardName);
    }
    ```

7. Now we can use the list of cards from the API rather than our hard-coded list of two cards. In CardTests, put this above our first test:
    ```c#
    static IList<Card> apiCards = new ApiCardService().GetAllCards();
    ```

8. Our tests can now leverage this list of cards using the `[TestCaseSource]` attribute. Our first test should then look like:

    ```c#
    [Test, Category("cards")]
    [TestCaseSource("apiCards")]
    [Parallelizable(ParallelScope.Children)]
    public void Card_is_on_Cards_Page(Card card)
    {
        var cardOnPage = Pages.Cards.Goto().GetCardByName(card.Name);
        Assert.That(cardOnPage.Displayed);
    }
    ```
    - We have added the "cards" Category to the test
    - The TestCaseSource is now coming from "apiCards"
    - We've added the same [Parallelizable] attribute to this test
    - Changed the test name to better reflect that it tests all cards and not just Ice Spirit anymore
    - The object we are passing into the test is not a `string`, but a `Card` object.

9. Update the second test to also use `apiCards`

10. Run the tests but use the `NUnit.NumberOfTestWorkers=2` argument so you don't overload your machine
    ```bash
    $ dotnet test --filter testcategory=cards -- NUnit.NumberOfTestWorkers=2
    ```
    - This will only run two tests at a time which is probably best for your machine
    - You can press `CTRL + C` in the terminal to cancel the test execution

11. CHALLENGE 1: After some test failures, you will see some interesting errors. The biggest one is that "Troop" is not equal to "tid_card_type_character" and "Spell" is not equal to "tid_card_type_spell".

The challenge is to solve this error.

> HINT: `tid_card_type_spell` already has the word "spell" in it. Could we use that somehow?
> HINT: `Troop` and `character` are the same thing in the context of the game. We should treat characters as troops and vice-versa

12. CHALLENGE 2: Similar to Challenge 1, tests will be failing because "Arena 8" is not equal to `8`

The challenge is to solve this error.

> HINT: A string of `"8"` is different than an integer of `8`
> HINT: Be aware that some cards have an Arena of `0` because they are playable in the "Training Camp". This is why we need to understand the data we're working with. Otherwise, you wouldn't be testing for those values! Check out the `Baby Dragon` card.

Since the recording, they have slightly changed the way their pages load. Because of this, you may experience "flakiness" because we are lacking any waits. We will be discussing waits in Chapter 7 and 12.


## Chapter 7 - New Test Suite

#### 7a

1. Make a new suite of tests by creating a new test file called `CopyDeckTests.cs`

2. Within this file, we'll copy and paste our SetUp and TearDown from the `CardTests.cs`

3. Then we can write a new test that checks whether a user can copy a deck

    ```c#
    [Test]
    public void User_can_copy_the_deck()
    {
        // 2. go to Deck Builder page
        Driver.FindElement(By.CssSelector("[href='/deckbuilder']")).Click();
        // 3. Click "add cards manually"
        Driver.FindElement(By.XPath("//a[text()='add cards manually']")).Click();
        // 4. Click Copy Deck icon
        Driver.FindElement(By.CssSelector(".copyButton")).Click();
        // 5. Click Yes
        Driver.FindElement(By.Id("button-open")).Click();
        // 6. Assert the "if click Yes..." message is displayed
        var copyMessage = Driver.FindElement(By.CssSelector(".notes.active"));
        Assert.That(copyMessage.Displayed);
    }
    ```

4. With the test written, we can now refactor this into our Page Map Pattern.
    - Create a `DeckBuilderPage.cs` file within `Royale.Pages`
    - Create the Page Map
    ```c#
    public class DeckBuilderPageMap
    {
        public IWebElement AddCardsManuallyLink => Driver.FindElement(By.CssSelector(""));

        public IWebElement CopyDeckIcon => Driver.FindElement(By.XPath("//a[text()='add cards manually']"));
    }
    ```

    - Then create the Page
    ```c#
    public class DeckBuilderPage : PageBase
    {
        public readonly DeckBuilderPageMap Map;

        public DeckBuilderPage()
        {
            Map = new DeckBuilderPageMap();
        }

        public DeckBuilderPage Goto()
        {
            HeaderNav.Map.DeckBuilderLink.Click();
            return this;
        }

        public void AddCardsManually()
        {
            Map.AddCardsManuallyLink.Click();
        }

        public void CopySuggestedDeck()
        {
            Map.CopyDeckIcon.Click();
        }
    }
    ```

5. Create the `CopyDeckPage.cs` file

6. Add these pages to the Pages Wrapper class in `Pages.cs`. Your Pages class should now look like this

    ```c#
    public class Pages
    {
        [ThreadStatic]
        public static CardsPage Cards;

        [ThreadStatic]
        public static CardDetailsPage CardDetails;

        [ThreadStatic]
        public static DeckBuilderPage DeckBuilder;

        [ThreadStatic]
        public static CopyDeckPage CopyDeck;

        public static void Init()
        {
            Cards = new CardsPage();
            CardDetails = new CardDetailsPage();
            DeckBuilder = new DeckBuilderPage();
            CopyDeck = new CopyDeckPage();
        }
    }
    ```

7. Use these pages in the test

    ```c#
    [Test]
    public void User_can_copy_the_deck()
    {
        Pages.DeckBuilder.Goto();
        Pages.DeckBuilder.AddCardsManually();
        Pages.DeckBuilder.CopySuggestedDeck();

        Pages.CopyDeck.Yes();

        Assert.That(Pages.CopyDeck.Map.CopiedMessage.Displayed);
    }
    ```

8. If you run the test, it fails pretty quickly! That's because the test in the code is running faster than the page is moving. We need to use `WebDriverWait`. Add this to the top of our test.
    ```c#
    var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(10));
    ```

9. After `Pages.DeckBuilder.AddCardsManually` add
    ```c#
    wait.Until(drvr => Pages.DeckBuilder.Map.CopyDeckIcon.Displayed);
    ```

10. Add another wait after `Pages.CopyDeck.Yes`
    ```c#
    wait.Until(drvr => Pages.CopyDeck.Map.CopiedMessage.Displayed);
    ```

11. Just like how we created our own Driver class to "extend" WebDriver, we will do the same for WebDriverWait.
    - Create a `Wait.cs` file in `Framework.Selenium`
    ```c#
    public class Wait
    {
        private readonly WebDriverWait _wait;

        public Wait(int waitSeconds)
        {
            _wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(waitSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };

            _wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(StaleElementReferenceException)
            );
        }

        public bool Until(Func<IWebDriver, bool> condition)
        {
            return _wait.Until(condition);
        }
    }
    ```

12. Include our new Wait to the Driver class

    ```c#
    [ThreadStatic]
    private static IWebDriver _driver;

    [ThreadStatic]
    public static Wait Wait;

    public static void Init()
    {
        _driver = new ChromeDriver(Path.GetFullPath(@"../../../../" + "_drivers"));
        Wait = new Wait(10);
    }
    ```

13. In our test, replace `wait.Until()` with our new Wait. For example:
    ```c#
    Driver.Wait.Until(drvr => Pages.DeckBuilder.Map.CopyDeckIcon.Displayed);
    ```

14. CHALLENGE 7a: On the `DeckBuilderPage`, the Page Map has two elements that may have incorrect locators. You probably didn't spot this!

Part 1 of the challenge is to validate that they work in the DevTools Console and fix them if needed.

Also, you may need to add another wait for the `AddCardsManually()` step.

Part 2 of the challenge is to add this missing wait to the test.


#### 7b

1. Refactor the Waits from our `User_can_copy_deck()` test into their respective actions. For example, the `AddCardsManually()` method should now look like:

    ```c#
    public void AddCardsManually()
    {
        Map.AddCardsManuallyLink.Click();
        Driver.Wait.Until(drvr => Map.CopyDeckIcon.Displayed);
    }
    ```

2. Write the next two tests with how we want them to eventuall look like. This is kind of a TDD approach:

    ```C#
    [Test]
    public void User_opens_app_store()
    {
        Pages.DeckBuilder.Goto().AddCardsManually();
        Pages.DeckBuilder.CopySuggestedDeck();
        Pages.CopyDeck.No().OpenAppStore();
        Assert.That(Driver.Title, Is.EqualTo("Clash Royale on the App Store"));
    }
    [Test]
    public void User_opens_google_play()
    {
        Pages.DeckBuilder.Goto().AddCardsManually();
        Pages.DeckBuilder.CopySuggestedDeck();
        Pages.CopyDeck.No().OpenGooglePlay();
        Assert.AreEqual("Clash Royale - Apps on Google Play", Driver.Title);
    }
    ```

    - Change the return type of our `DeckBuilder.Goto()` method so we can "chain" `AddCardsManually()`
    - From `void` to `DeckBuilderPage` and adding `return this;`

    ```c#
    public DeckBuilder Goto()
    {
        HeaderNav.Map.DeckBuilderLink.Click();
        Driver.Wait.Until(drvr => Map.AddCardsManuallyLink.Displayed);
        return this;
    }
    ```

3. You will have some errors like a red squiggly under `No()` because we haven't implemented it yet. Let's do that! In the `CopyDeckPage`, add a No() method:

    ```c#
    public CopyDeckPage No()
    {
        Map.NoButton.Click();
        return this;
    }
    ```

    - `return this;` returns the current instance of itself - the CopyDeckPage
    - Notice how the return type is a `CopyDeckPage`
    - This allows us to "chain" commands and actions like we did in Step 2!

4. Add the `OpenAppStore()` and `OpenGooglePlay()` methods as well

    ```c#
    public void OpenAppStore()
    {
        Map.AppStoreButton.Click();
    }

    public void OpenGooglePlay()
    {
        Map.GooglePlayButton.Click();
    }
    ```

    - Find the elements and add them to the `CopyDeckPageMap` so you can access them in the above methods

5. Fix the last error which is `Driver.Title` in the test. Like the error suggests, our `Driver` class doesn't have a `Title` property. Let's add it!

    ```c#
    public static string Title => Current.Title;
    ```

6. The Accept Cookies banner at the bottom of the Copy Deck page will overlap our App Store buttons. We need to accept this to remove the banner. Add this method:

    ```c#
    public void AcceptCookies()
    {
        Map.AcceptCookiesButton.Click();
        Driver.Wait.Until(drvr => !Map.AcceptCookiesButton.Displayed);
    }
    ```

    - Find the element and add it to the Map
    - The `AcceptCookies()` method will click the button and then wait for it to disappear (aka NOT BE displayed)

7. Add the AcceptCookies() to our No()

    ```c#
    public CopyDeckPage No()
    {
        Map.NoButton.Click();
        AcceptCookies();
        Driver.Wait.Until(drvr => Map.OtherStoresButton.Displayed);
        return this;
    }
    ```

    - We also added a Wait to make sure the `OtherStoresButton` is displayed first before proceeding

8. CHALLENGE 7b: At the end of the video, I show you that our `User_opens_app_store()` fails because of a string comparison issue:

`"Clash Royale on the App Store"`

is not equal to

`"Clash Royale on the App Store"`

They look identical, but there is a big difference. The string we get back from `Driver.Title` has a *unicode* character at the beginning!

- The challenge is to solve this error

> NOTE: There are many ways to approach this, but remember that this is String Comparison


## Chapter 8 - Logging

1. Create a `FW.cs` in the Framework project
- This is going to hold objects that are used throughout our Framework and projects
- We'll start with the the workspace directory and test results directory

    ```c#
    public static string WORKSPACE_DIRECTORY = Path.GetFullPath(@"../../../../");

    public static DirectoryInfo CreateTestResultsDirectory()
    {
        var testDirectory = WORKSPACE_DIRECTORY + "TestResults";

        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, recursive: true);
        }

        return Directory.CreateDirectory(testDirectory);
    }
    ```

2. Create a `Logging` directory with the `Framework` project and create a file called `Logger.cs`

3. The Logger class will handle the `log.txt` and writing to them

    ```c#
    public class Logger
    {
        private readonly string _filepath;

        public Logger(string testName, string filepath)
        {
            _filepath = filepath;

            using (var log = File.CreateText(_filepath))
            {
                log.WriteLine($"Starting timestamp: {DateTime.Now.ToLocalTime()}");
                log.WriteLine($"Test: {testName}");
            }
        }

        public void Info(string message)
        {
            WriteLine($"[INFO]: {message}");
        }

        public void Step(string message)
        {
            WriteLine($"    [STEP]: {message}");
        }

        public void Warning(string message)
        {
            WriteLine($"[WARNING]: {message}");
        }

        public void Error(string message)
        {
            WriteLine($"[ERROR]: {message}");
        }

        public void Fatal(string message)
        {
            WriteLine($"[FATAL]: {message}");
        }

        private void WriteLine(string text)
        {
            using (var log = File.AppendText(_filepath))
            {
                log.WriteLine(text);
            }
        }

        private void Write(string text)
        {
            using (var log = File.AppendText(_filepath))
            {
                log.Write(text);
            }
        }
    }
    ```

    - `WriteLine()` and `Write()` will make writing to the correct log.txt file a piece of cake
    - Different "log types" are used depending on the type of message we want to display
        - Info
        - Step
        - Warning
        - Error
        - Fatal
    - Whenever we make a new instance of Logger(), it will create a new log file using the `testName` and `filepath` that are passed.

4. Our `Framework` project needs to use NUnit, so we will use PackSharp
- Open Command Palette > `PackSharp: Add Package` > select `Framework` > search "NUnit" > select `NUnit`
- Open Command Palette > `PackSharp: Remove Package` > select `Royale.Tests` > select `NUnit`
- Build solution to make sure things are still structured ok:

    ```bash
    $ dotnet clean
    $ dotnet restore
    $ dotnet build
    ```

5. Add a `SetLogger()` method to our `FW` class to create the Logger per test. We'll also need some fields and properties to hold these values. We will also use a `lock` to solve any "race conditions":

    ```c#
    public static Logger Log => _logger ?? throw new NullReferenceException("_logger is null. SetLogger() first.");

    [ThreadStatic]
    public static DirectoryInfo CurrentTestDirectory;

    [ThreadStatic]
    private static Logger _logger;

    public static void SetLogger()
    {
        lock (_setLoggerLock)
        {
            var testResultsDir = WORKSPACE_DIRECTORY + "TestResults";
            var testName = TestContext.CurrentContext.Test.Name;
            var fullPath = $"{testResultsDir}/{testName}";

            if (Directory.Exists(fullPath))
            {
                CurrentTestDirectory = Directory.CreateDirectory(fullPath + TestContext.CurrentContext.Test.ID);
            }
            else
            {
                CurrentTestDirectory = Directory.CreateDirectory(fullPath);
            }

            _logger = new Logger(testName, CurrentTestDirectory.FullName + "/log.txt");
        }
    }

    private static object _setLoggerLock = new object();
    ```

6. In the Test Suites, `CopyDeckTests` and `CardTests`, add a `[OneTimeSetup]` method and update the `[SetUp]` method:

    ```c#
    [OneTimeSetUp]
    public void BeforeAll()
    {
        FW.CreateTestResultsDirectory();
    }

    [SetUp]
    public void BeforeEach()
    {
        FW.SetLogger();
        Driver.Init();
        Pages.Init();
        Driver.Goto("https://statsroyale.com");
    }
    ```

    - `[OneTimeSetUp]` is run before any tests. This will create the `TestResults` directory for the test run.
    - `FW.SetLogger()` will create an instance of Logger, which creates a log.txt file, for each test.

7. Run the tests

8. Open the new `TestResults` directory. You will see that there are directories created for each test and each test has its own `log.txt` file!


## Chapter 9 - Element and Elements

1. Create an `Element.cs` under `Framework.Selenium`

    ```c#
    public class Element : IWebElement
    {
        private readonly IWebElement _element;

        public readonly string Name;

        public By FoundBy { get; set; }

        public Element(IWebElement element, string name)
        {
            _element = element;
            Name = name;
        }

        public IWebElement Current => _element ?? throw new System.NullReferenceException("_element is null.");

        public string TagName => Current.TagName;

        public string Text => Current.Text;

        public bool Enabled => Current.Enabled;

        public bool Selected => Current.Selected;

        public Point Location => Current.Location;

        public Size Size => Current.Size;

        public bool Displayed => Current.Displayed;

        public void Clear()
        {
            Current.Clear();
        }

        public void Click()
        {
            FW.Log.Step($"Click {Name}");
            Current.Click();
        }

        public IWebElement FindElement(By by)
        {
            return Current.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Current.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return Current.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Current.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return Current.GetProperty(propertyName);
        }

        public void SendKeys(string text)
        {
            Current.SendKeys(text);
        }

        public void Submit()
        {
            Current.Submit();
        }
    }
    ```

    - Our class `Element` will implement the `IWebElement` interface
    - Just like our Driver, `Current` will represent the current instance of the IWebElement we're extending
    - Add logging to our `Click()` method

2. Our Driver should now return `Element` instead of `IWebElement`

    ```c#
    public static Element FindElement(By by, string elementName)
    {
        return new Element(Current.FindElement(by), elementName)
        {
            FoundBy = by
        };
    }
    ```

3. Go to each of our Pages and change our Maps to use `Element` instead of `IWebElement` and give each element a name. For example, in `HeaderNav.cs`:

    ```c#
    public class HeaderNavMap
    {
        public Element CardsTabLink => Driver.FindElement(By.CssSelector("a[href='/cards']"), "Cards Link");

        public Element DeckBuilderLink => Driver.FindElement(By.CssSelector("a[href='/deckbuilder']"), "Deck Builder Link");
    }
    ```

    > NOTE: The `elementName` we pass in, like "Cards Link", is used for logging purposes

4. Now create `Elements.cs` under `Framework.Selenium`

    ```c#
    public class Elements : ReadOnlyCollection<IWebElement>
    {
        private readonly IList<IWebElement> _elements;

        public Elements(IList<IWebElement> list) : base(list)
        {
            _elements = list;
        }

        public By FoundBy { get; set; }

        public bool IsEmpty => Count == 0;
    }
    ```

    - Our class already has access to things like `Count` because it's inheriting from `ReadOnlyCollection<IWebElement>`
    - We already have the functionality of a list, but now we can add our own! `FoundBy` and `IsEmpty` are examples of this

5. Our Driver should return `Elements` instead of `IList<IWebElement>`

    ```c#
    public static Elements FindElements(By by)
    {
        return new Elements(Current.FindElements(by))
        {
            FoundBy = by
        };
    }
    ```

6. CHALLENGE: Our Element class can now have any functionality we want! This is a very powerful way for you to control what you can and cannot do with your elements.

- The challenge is to add a `Hover()` method so that each element simply call .Hover()

> HINT: The Actions class is in `OpenQA.Selenium.Interactions`


## Chapter 10 - Configuration

1. Create `DriverFactory.cs` under `Framework.Selenium`

    ```c#
    public static class DriverFactory
    {
        public static IWebDriver Build(string browserName)
        {
            FW.Log.Info($"Browser: {browserName}");

            switch (browserName)
            {
                case "chrome":
                    return new ChromeDriver(FW.WORKSPACE_DIRECTORY + "_drives");

                case "firefox":
                    return new FirefoxDriver();

                default:
                    throw new System.ArgumentException($"{browserName} not supported.");
            }
        }
    }
    ```

    - This handles creating our ChromeDriver, but also allows you to define how you want your different drivers to be created

2. Use this is our Driver

    ```c#
    public static void Init(string browserName)
    {
        _driver = DriverFactory.Build(browserName);
        Wait = new Wait(10);
    }
    ```

3. Update our `[SetUp]` methods since we now require a browserName in `Driver.Init()`

    ```c#
    [SetUp]
    public void BeforeEach()
    {
        FW.SetLogger();
        Driver.Init("chrome");
        Pages.Init();
        Driver.Goto("https://statsroyale.com");
    }
    ```

4. Create a `framework-config.json` at the workspace root

    ```javascript
    {
        "driver": {
            "browser": "chrome"
        },

        "test": {
            "url": "staging.statsroyale.com"
        }
    }
    ```

5. Create `FwConfig.cs` under `Framework` to represent the json in code:

    ```c#
    public class FwConfig
    {
        public DriverSettings Driver { get; set; }

        public TestSettings Test { get; set; }
    }

    public class DriverSettings
    {
        public string Browser { get; set; }
    }

    public class TestSettings
    {
        public string Url { get; set; }
    }
    ```

    - `DriverSettings` represents the "driver" object in the json
    - `TestSettings` represents the "test" object in the json
    - `FwConfig` represents the entire config file

6. Add a "singleton" representation of our config in the `FW` class

    ```c#
    public static FwConfig Config => _configuration ?? throw new NullReferenceException("Config is null. Call FW.SeConfig() first.");

    private static FwConfig _configuration;
    ```

7. Now add the `SetConfig()` method

    ```c#
    public static void SetConfig()
    {
        if (_configuration == null)
        {
            var jsonStr = File.ReadAllText(WORKSPACE_DIRECTORY + "/framework-config.json");
            _configuration = JsonConvert.DeserializeObject<FwConfig>(jsonStr);
        }
    }
    ```

    - We check if the _configuration is null
        - if null, then set it
        - if not null, then it has already been set
    - We are getting the .json file and turning it into a json string
    - Then we deserialize it into the shape of our `FwConfig` so it can be used in code

8. Go back to our Driver class and use the config

    ```c#
    public static void Init()
    {
        _driver = DriverFactory.Build(FW.Config.Driver.Browser);
        Wait = new Wait(10);
    }
    ```

9. Update our test files to use the config too

    ```c#
    [OneTimeSetUp]
    public void BeforeAll()
    {
        FW.SetConfig();
        FW.CreateTestResultsDirectory();
    }

    [SetUp]
    public void BeforeEach()
    {
        FW.SetLogger();
        Driver.Init();
        Pages.Init();
        Driver.Goto(FW.Config.Test.Url);
    }
    ```

    - `FW.SetConfig();` in the [OneTimeSetUp]
    - `Driver.Goto(FW.Config.Test.Url);` in the [SetUp]

10. Run the `copydeck` tests

11. They failed! Take a look at the log files to see what happened

    - Our test logged this: `[INFO]: http://staging.statsroyale.com`
    - It tried navigating to a "staging" version of the site, but we don't have access

12. Update the `test.url` value in our `framework-config.json`

    ```javascript
    {
        "driver": {
            "browser": "chrome"
        },

        "test": {
            "url": "statsroyale.com"
        }
    }
    ```

13. CHALLENGE: I bet you already have some ideas for things we could put into our framework-config.json. For this challenge, add a "wait" property to hold the default number of seconds that we want to use when instantiating our Wait class.

> HINT: We're instantiating Wait in Driver.Init()


## Chapter 11 - TestBase and Outcomes

1. Create a `Base` folder within `Royale.Tests`

2. Create `TestBase.cs` within that `Base` folder

3. In one of our test suites, copy the `[OneTimeSetUp]`, `[SetUp]` and `[TearDown]` methods and paste them into our TestBase class

    ```c#
    public abstract class TestBase
    {
        [OneTimeSetUp]
        public virtual void BeforeAll()
        {
            FW.SetConfig();
            FW.CreateTestResultsDirectory();
        }

        [SetUp]
        public virtual void BeforeEach()
        {
            FW.SetLogger();
            Driver.Init();
            Pages.Init();
            Driver.Goto(FW.Config.Test.Url);
        }

        [TearDown]
        public virtual void AfterEach()
        {
            Driver.Quit();
        }
    }
    ```

    - TestBase is an abstract class

4. In our test suites, delete the `[OneTimeSetUp]`, `[SetUp]` and `[TearDown]` methods

5. Our test classes should now inherit TestBase

    ```c#
    public class CopyDeckTests : TestBase
    ```

6. Run a test and you'll see nothing has changed!

7. In our `[TearDown]` method, now in TestBase, we'll handle the outcome of the test

    ```c#
    [TearDown]
    public virtual void AfterEach()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;

        if (outcome == TestStatus.Passed)
        {
            FW.Log.Info("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            FW.Log.Info("Outcome: Failed");
        }
        else
        {
            FW.Log.Warning("Outcome: " + outcome);
        }

        Driver.Quit();
    }
    ```

8. We want to take a screenshot if our test fails. Let's add that functionality somewhere in our Driver class

    ```c#
    public static void TakeScreenshot(string imageName)
    {
        var ss = ((ITakesScreenshot)Current).GetScreenshot();
        var ssFileName = Path.Combine(FW.CurrentTestDirectory.FullName, imageName);
        ss.SaveAsFile($"{ssFileName}.png", ScreenshotImageFormat.Png);
    }
    ```

9. Now add `TakeScreenshot()` when our test fails

    ```c#
    else if (outcome == TestStatus.Failed)
    {
        Driver.TakeScreenshot("test_failed");
        FW.Log.Info("Outcome: Failed");
    }
    ```

10. Run a test that fails. You may need to force a failure with `Assert.Fail();`

11. Open the test's result directory
    - There should be a `test_failed.png` along with the `log.txt`

#### BONUS: Maximizing a Window

We touched on this in Chapter 2. For Windows, it's pretty easy:

```c#
public IWebDriver BuildChrome()
{
    var options = new ChromeOptions();
    options.AddArgument("--start-maximized");
    return new ChromeDriver(options);
}
```

or

```c#
driver.Manage().Window.Maximize();
```

However, this may not work for Mac or Linux. Let's go over a method that works cross-platform.

1. Create `Window.cs` under `Framework.Selenium`

2. We'll add a few methods to start out

    ```c#
    public ReadOnlyCollection<string> CurrentWindows => Driver.Current.WindowHandles;

    public void SwitchTo(int windowIndex)
    {
        Driver.Current.SwitchTo().Window(CurrentWindows[windowIndex]);
    }
    ```

3. Add a property to get the current ScreenSize so we know the amount of space we're working with

> NOTE: This is different than what's in the video because of changes made to .NET Standard and Selenium

    ```c#
    public Size ScreenSize
    {
        get
        {
            var js = "return [window.screen.availWidth, window.screen.availHeight];";
            var jse = (IJavaScriptExecutor)Driver.Current;

            dynamic dimensions = jse.ExecuteScript(js, null);
            var x = Convert.ToInt32(dimensions[0]);
            var y = Convert.ToInt32(dimensions[1]);

            return new Size(x, y);
        }
    }
    ```

4. Create a `Maximize()` method that uses the ScreenSize

    ```c#
    public void Maximize()
    {
        Driver.Current.Manage().Window.Position = new Point(0, 0);
        Driver.Current.Manage().Window.Size = ScreenSize;
    }
    ```

5. Add the Window class to our Driver

    ```c#
    [ThreadStatic]
    public static Window Window;

    public static void Init()
    {
        _driver = DriverFactory.Build(FW.Config.Driver.Browser);
        Wait = new Wait(FW.Config.Driver.WaitSeconds);
        Window = new Window();
        Window.Maximize();
    }
    ```

6. Run a test. It should spin up a browser and then maximize!


## Chapter 12 - Wait Conditions

1. Install the `Selenium Extras` package with PackSharp

    - Open Command Palette > `PackSharp: Add Package` > select `Framework` > search "Selenium Extras" > select `DotNetSeleniumExtras.WaitHelpers`

2. Use ExpectedConditions in our No() method

> NOTE: Make sure to use it from the `SeleniumExtras.WaitHelpers` and NOT from `OpenQA.Selenium.Support.UI`

    ```c#
    Driver.Wait.Until(ExpectedConditions.ElementIsVisible(Map.OtherStoresButton.FoundBy));
    ```

3. We need to solve the error by adding an overload of Until() in our Wait class

    ```c#
    public IWebElement Until(Func<IWebDriver, IWebElement> condition)
    {
        return _wait.Until(condition);
    }
    ```

4. Our No() method should now look like this

    ```c#
    public CopyDeckPage No()
    {
        Map.NoButton.Click();
        AcceptCookies();
        Driver.Wait.Until(ExpectedConditions.ElementIsVisible(Map.OtherStoresButton.FoundBy));
        return this;
    }
    ```

5. Create `WaitConditions.cs` under `Framework.Selenium`

    ```c#
    public sealed class WaitConditions
    {
        public static Func<IWebDriver, bool> ElementDisplayed(IWebElement element)
        {
            bool condition(IWebDriver driver)
            {
                return element.Displayed;
            }

            return condition;
        }
    }
    ```

6. Use our WaitConditions in the DeckBuilderPage's `AddCardsManually()` method

    ```c#
    public void AddCardsManually()
    {
        Driver.Wait.Until(WaitConditions.ElementDisplayed(Map.AddCardsManuallyLink));
        Map.AddCardsManuallyLink.Click();
        Driver.Wait.Until(WaitConditions.ElementDisplayed(Map.CopyDeckIcon));
    }
    ```

7. We also need to handle elements that need to disappear. Let's add it to our WaitConditions class

    ```c#
    public static Func<IWebDriver, bool> ElementNotDisplayed(IWebElement element)
    {
        bool condition(IWebDriver driver)
        {
            try
            {
                return !element.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        }

        return condition;
    }
    ```

8. Now we'll use this in our `AcceptCookies()` method in the CopyDeckPage

    ```c#
    public void AcceptCookies()
    {
        Map.AcceptCookiesButton.Click();
        Driver.Wait.Until(WaitConditions.ElementNotDisplayed(Map.AcceptCookiesButton));
    }
    ```

9. With our Waits, we can return more than just booleans. Let's create a WaitCondition that returns a list of Elements once at least one of them is found. Add this to our WaitConditions class:

    ```c#
    public static Func<IWebDriver, Elements> ElementsNotEmpty(Elements elements)
    {
        Elements condition(IWebDriver driver)
        {
            Elements _elements = Driver.FindElements(elements.FoundBy);
            return _elements.IsEmpty ? null : _elements;
        }

        return condition;
    }
    ```

10. Add another Wait that returns an element instead of just bool. This WaitCondition will solve us waiting for an element to be displayed on one line, and then clicking it in the next line.

    ```c#
    public static Func<IWebDriver, IWebElement> ElementIsDisplayed(IWebElement element)
    {
        IWebElement condition(IWebDriver driver)
        {
            try
            {
                return element.Displayed ? element : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (ElementNotVisibleException)
            {
                return null;
            }
        }

        return condition;
    }
    ```

11. Now let's use it in the AddCardsManually() method

    ```c#
    public void AddCardsManually()
    {
        Driver.Wait.Until(
            WaitConditions.ElementIsDisplayed(Map.AddCardsManuallyLink))
            .Click();
        Driver.Wait.Until(WaitConditions.ElementDisplayed(Map.CopyDeckIcon));
    }
    ```

12. CHALLENGE: I didn't run the test just so we could do this challenge :)

When working with web elements, we usually only work with elements that we expect to be present on the DOM. At least, that's what our users do. Because of this, it makes sense for us to "wait" for elements to at least be present in the DOM before returning it.

In this challenge, you need to add a "Wait" to our `Driver.FindElement()` method so every time we "find" an element, we wait until it exists before returning it.

> HINT: Give it a try, but you can look at the last commit to see how this is done.
> HINT: Your tests will most likely fail on the `AddCardsManually()` step because when we call `Map.AddCardsManuallyButton`, we are having the Driver find the element at that time instead of within a Wait. Solving this challenge will also solve that issue.

That's it! You are now done with the course :)
