using AutomationInfra.StringExtention;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutomationInfra.ExtensionsMethods
{
    public static class WebDriverExtension
    {
        public static void WaitForStaleElementError()
        {
            Thread.Sleep(400);
        }          

        public static string GetElementText(this IWebElement firstSearch, IWebDriver driver,
            By? by = null, int fromSeconds = 60)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(fromSeconds));

            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(ElementNotSelectableException),
                typeof(InvalidSelectorException),
                typeof(NoSuchFrameException),
                typeof(ElementNotInteractableException),
                typeof(WebDriverException));

            var elementText = "";
            IWebElement webElement;
            var newString = "";

            //try
            //{
            var actualElementText = wait.Until(d =>
            {
                try
                {
                    if (by != null)
                    {
                        webElement = driver.FindElement(by);
                    }
                    else
                    {
                        webElement = firstSearch;
                    }

                    newString = ((IJavaScriptExecutor)driver)
                        .ExecuteScript("return arguments[0].innerText;", webElement)?
                        .ToString();

                    //newString = webElement.Text;

                    if (string.IsNullOrEmpty(newString))
                    {
                        newString = ((IJavaScriptExecutor)driver)
                        .ExecuteScript("return arguments[0].value;", webElement)?
                        .ToString();

                        return newString ?? "";
                    }

                    if (newString != null && !string.IsNullOrEmpty(newString))
                    {
                        var temp = newString.Trim().RemoveNewLine();
                        newString = temp;
                    }

                    return newString;
                }
                catch (StaleElementReferenceException)
                {
                    //Console.WriteLine("Stale Element");
                    WaitForStaleElementError();

                    return null;
                }
            });

            if (elementText == null)
            {
                //Console.WriteLine($"element Text is null: {elementText}");
            }

            return newString ?? "";
        }

        public static void SendsKeysAuto(this IWebElement element,
          IWebDriver driver, By by, string input,
          int fromSeconds = 60)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(fromSeconds));

            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(ElementNotSelectableException),
                typeof(InvalidSelectorException),
                typeof(NoSuchFrameException),
                typeof(InvalidElementStateException),
                typeof(WebDriverException));

            wait.Until(d =>
            {
                try
                {
                    if (driver == null)
                    {
                        return false;
                    }

                    var element = driver.FindElement(by);

                    if (element == null)
                    {
                        return false;
                    }

                    var elementText = element.GetElementText(driver, by);

                    if (!input.Equals(elementText))
                    {
                        element.Clear();
                        Thread.Sleep(15);
                        elementText = element.GetElementText(driver, by);
                        element.SendKeys(input);
                        Thread.Sleep(15);
                        elementText = element.GetElementText(driver);

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    WaitForStaleElementError();
                    Console.WriteLine("SendsKeys Auto, Stale Element Reference Exception");

                    return false;
                }
                catch (ElementNotInteractableException)
                {
                    Console.WriteLine("Element Not Intractable Exception");

                    return false;
                }
            });

        }

        public static IWebElement SearchElement(this IWebDriver driver, By by,
            int fromSeconds = 60, string? log = null)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(fromSeconds));

            wait.IgnoreExceptionTypes(
                typeof(ElementNotVisibleException),
                typeof(ElementNotSelectableException),
                typeof(InvalidSelectorException),
                typeof(NoSuchFrameException),
                typeof(ElementNotInteractableException),
                typeof(WebDriverException));

            IWebElement? elementToFind = null;

            try
            {
                var js = (IJavaScriptExecutor)driver;

#pragma warning disable CS8603 // Possible null reference return.
                return wait.Until(d =>
                {
                    try
                    {
                        if (driver == null)
                        {
                            return null;
                        }

                        elementToFind = driver.FindElement(by);

                        if (elementToFind == null)
                        {
                            return null;
                        }

                        if (!elementToFind.Enabled)
                        {
                            return null;
                        }

                        return elementToFind;
                    }
                    catch (StaleElementReferenceException)
                    {
                        WaitForStaleElementError();

                        return null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception ex)
            {
                var exceMessage = $" search message: {log}, Search Element method" +
                    $" search parameters: {by?.ToString()} ," +
                    $" Exception: {ex?.Message} ";

                var newException = new Exception(exceMessage);

                throw newException;
            }
        }           

        public static void ForceClick(this IWebElement element, IWebDriver driver,
            By? by = null, int fromSeconds = 60)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(fromSeconds));
            var actions = new Actions(driver);

            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(ElementNotSelectableException),
                typeof(InvalidSelectorException),
                typeof(NoSuchFrameException),
                typeof(ElementNotVisibleException),
                typeof(ElementClickInterceptedException),
                typeof(ElementNotInteractableException),
                typeof(WebDriverException));

            try
            {
                var js = (IJavaScriptExecutor)driver;

                wait.Until(d =>
                {
                    try
                    {
                        if (driver == null)
                        {
                            return false;
                        }

                        if (by != null)
                        {
                            element = driver.FindElement(by);
                        }

                        if (element.Enabled)
                        {
                            element.Click();
                        }

                        return true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        WaitForStaleElementError();

                        return false;
                    }                  
                    catch (MoveTargetOutOfBoundsException)
                    {
                        return true;
                    }
                });
            }
            catch (Exception ex)
            {
                var exceMessage = $" css selector: {by}, Exception: {ex?.Message}";

                throw new Exception(exceMessage);
            }
        }        
    }
}
