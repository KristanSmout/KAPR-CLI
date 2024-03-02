# KAPR-CLI 💻🚀 (Kristan's <Awesome|Alright|Awful> Pingdom Replacement)
KAPR-CLI is an on-premises web monitoring & testing tool, offering comprehensive testing capabilities through Selenium with Chrome. With KAPR-CLI, you can automate browser actions and test scenarios easily.

## Why KAPR?
KAPR was born out of frustration with Pingdom. Constant issues and unreliable support led to the creation of KAPR-CLI. By developing my own solution, I hope to provide a more dependable & customisaable alternative to Pingdom, ensuring that users can rely on their monitoring tools without constant interruptions and blame-shifting. This tool can also be used internally allowing critical but none exposed sites to be monitored.

## Installation
If you wish to use this project without compiling then please take the latest release, pingdom currently does *NOT* support automatic updates so you will need to obtain all new versions manually.

If you wish to compile this project yourself or want to make your own changes

*Gitea*
```
$ git clone https://git.kristansmout.co.uk/kristan/KAPR-CLI
$ cd KAPR-CLI
```

*Github*
```
$ git clone https://github.com/KristanSmout/KAPR-CLI
$ cd KAPR-CLI
```


## Usage
KAPR-CLI supports a range of functionalieis, firstly we will cover the launch arguments that can be provided

### Launch Arguments

| Arguments           | Description                                                    |
|---------------------|----------------------------------------------------------------|
| `-h`, `--help`      | Displays this help message.                                    |
| `-a`, `--arguments` | Displays a list of available commands.                          |
| `--version`         | Displays the current version of KAPR-CLI.                          |
| `-c`, `--config`    | Specifies the path to the KAPR configuration file.              |
| `-f`, `--functions` | Overrides the default function configuration file path.       |
| `-i`, `--instructions` | Specifies the path to the KAPR instruction file.                |

### Runtime Overrides:

| Arguments         | Description                                                              |
|------------------|--------------------------------------------------------------------------|
| `-o`, `--output`  | Specifies the output directory for test results.                             |
| `-l`, `--logging`  | Enables or disables logging (TRUE/FALSE).                                  |
| `-s`, `--screenshot` | Enables or disables forced screenshots (TRUE/FALSE).                 |
| `-t`, `--timeout` | Sets the timeout duration for tests in seconds.                          |
| `-e`, `--email`    | Specifies email recipient(s) for notifications (comma-separated).       |
| `-u`, `--useragent` | Enables or disables using a custom user agent (TRUE/FALSE).              |
| `-v`, `--visible`  | Enables or disables displaying the browser window (TRUE/FALSE).            |
| `-r`, `--resolution` | Sets the screen resolution for the browser window (format: X,Y).        |

### Configuration Overrides:

| Arguments            | Description                                                                         |
|------------------------|---------------------------------------------------------------------------------------------|
| `--smtpserver`         | Overrides the SMTP server address specified in the configuration file.                    |
| `--smtpport`           | Overrides the SMTP port specified in the configuration file.                             |
| `--smtpsender`         | Overrides the email sender address specified in the configuration file.                   |
| `--smtpusername`       | Overrides the username for SMTP authentication specified in the configuration file.         |
| `--smtppassword`       | Overrides the password for SMTP authentication specified in the configuration file.         |
| `--smtpenablessl`       | Overrides the SSL/TLS encryption setting for the SMTP server specified in the configuration file. |


## Supported Actions
KAPR-CLI offers a variety of actions to automate browser interactions and testing scenarios. This section provides a detailed overview of these actions, outlining their syntax, usage examples, and important considerations.

### Locator Types
| Locator Type | Description                                                                           | Example                                                |
|--------------|------------------------------------------------------------------------------------------|-------------------------------------------------------|
| `Id`          | Clicks an element based on its unique ID attribute.                                    | `click id SubmitButton`                               |
| `CssSelector` | Selects an element using a specific CSS selector.                                          | `click CssSelector .my-button`                      |
| `XPath`         | Locates an element using an XPath expression.                                                | `click XPath //button[@name='submit']`             |
| `Text`         | Clicks an element containing the specified text.                                          | `click Text "Click Me"`                             |
| `LinkText`    | Clicks a link element with the exact text.                                                 | `click LinkText "Learn More"`                         |
| `PartialLinkText` | Clicks a link containing the specified text (regardless of full text).                   | `click PartialLinkText "Click Here"`                |
| `Name`         | Clicks an element by its `name` attribute.                                               | `click Name "searchButton"`                           |
| `TagName`      | Clicks the first element of a specific tag name.                                           | `click TagName input[type="submit"]`                 |
| `JavaScript`   | Executes custom JavaScript code to click an element.                                    | `click JavaScript $('button#myButton').click()`       |

### Actions
The follow are actions that you can use directly in your configuration file to create a customised plan

### **Click:**
This action allows you to interact with buttons on web pages using various locator strategies supported by Selenium.
**Syntax:**
```
click <locator_type> {locator_value}
```
**Examples:**
```
click id SubmitButton
```
```
click CssSelector .my-button
```

## Navigate

**Description:** Navigates the browser to a specified URL.

**Syntax:**
```
navigate <url>
```
**Example:**
```
navigate kristansmout.co.uk
```
- Consider using a link instead of `navigate` if your intention is to navigate the user to another page within your own website. This improves accessibility and usability.

## Screenshot
**Description:** Captures a screenshot of the browser window and saves it with the given name.

**Syntax:**
```
screenshot <name>
```
**Example:**
```
screenshot HomePage
```
- Replace my_screenshot with the desired filename for the screenshot.

### Snapshot

**Description:** Takes a snapshot of the current HTML content of the page and saves it with the given name.

**Syntax:**
```
snapshot <name>
```
**Example:**
```
snapshot PageSnapshot
```
- The snapshot captures the HTML content as it is at the moment the command is executed.

### SetValue

**Description:** Sets the value of a specified element on the webpage.

**Syntax:**
```
setvalue <locator_type> <locator_value> <text>
```
**Example:**
```
setvalue Id username johndoe@example.com
```
- Use this action to input text into input fields, text areas, etc.

### ReadValue

**Description:** Reads the value of a specified element on the webpage.

**Syntax:**
```
readvalue <locator_type> <locator_value>
```
**Example:**
```
readvalue CssSelector .username-input
```
- This action is useful for verifying input field values, retrieving text, etc.

### Sleep

**Description:** Pauses the execution for the specified duration in milliseconds.

**Syntax:**
```
sleep <milliseconds>
```
**Example:**
```
sleep 3000
```
- Use this action for adding delays in your automation scripts.

### WaitFor

**Description:** Waits for the specified element to meet the specified condition.

**Syntax:**
```
waitfor <locator_type> <locator_value> <existence_condition>
```
**Example:**
```
waitfor Id myElement Exists
```
- This action is helpful for synchronizing actions with page loading or element visibility.

### Write

**Description:** Outputs the given text to the debug console.

**Syntax:**
```
write <text>
```
**Example:**
```
write This is a debug message.
```
- Use this action for debugging purposes to output intermediate results or messages.

### URL

**Description:** Checks the current URL of the webpage.

**Syntax:**
```
url <match_condition> [expected_url]
```
**Examples:**
```
url Matches https://example.com
```
```
url Return
```
- Use this action for verifying or retrieving the URL of the webpage.

### JavaScript

**Description:** Executes JavaScript code in the browser.

**Syntax:**
```
javascript <query_type> <javascript_code>
```
**Example:**
```
javascript Run alert('Hello, world!');
```
- This action allows for executing custom JavaScript code within the browser context.

### WaitForLoad

**Description:** Waits for the page to finish loading.

**Syntax:**
```
waitforload
```
**Example:**
```
waitforload
```
- Use this action to ensure that the page has completely loaded before proceeding with further actions.

### Routine

**Description:** Executes a predefined routine or function.

**Syntax:**
```
routine <routine_name>
```
**Example:**
```
routine Login
```
- This action allows for the execution of reusable routines or functions within the automation script.


## Configuration
KAPR-CLI uses a optional configuration file this is *Currently* only used for allowing emails to be sent. This file is in a JSON format.
**Example:**
```
{
  "smtpServer": "smtp.test.com",
  "smtpPort": 587,
  "smtpSender": "test@Test.com",
  "smtpUsername": "test@test.com",
  "smtpPassword": "",
  "smtpEnableSSL": true
}
```

## Actions File
The actions file is the main configuration file for KAPR-CLI, this allows you to create and customise your check/testing across a range of different parameters.

```
{
  "Name": "Test",
  "logging": true,
  "configurationFilePath": "<Path to configuration file>",
  "functionFileDirectory": "<Path to function files directory>",
  "outputDirectory": "<Path to output directory>",
  "forceScreenshot": true,
  "timeout": 5,
  "sendEmail": true,
  "emailRecipientList": [
    "<Recipient Email 1>",
    "<Recipient Email 2>"
  ],
  "userAgent": "<User-Agent string>",
  "visible": false,
  "screenResolution": [
    "1920",
    "1080"
  ],
  "actions": [
    "navigate <URL>",
    "waitfor <locator_type> <locator_value> <existence_condition>",
    "sleep <milliseconds>",
    "click <locator_type> <locator_value>",
    "setvalue <locator_type> <locator_value> <text>",
    "click text <text>",
    "snapshot <name>",
    "click CssSelector <selector>",
    "click id <element_id>",
    "screenshot <name>",
    "click CssSelector <selector>",
    "click id <element_id>",
    "click CssSelector <selector>",
    "screenshot <name>",
    "click CssSelector <selector>",
    "sleep <milliseconds>"
  ]
}
```


**Explanation:**
- **Name**: Specifies the name of the test scenario.

- **logging**: Boolean value indicating whether logging is enabled for the test execution.

- **configurationFilePath**: Path to the configuration file containing additional settings needed for the test.

- **functionFileDirectory**: Directory path where the function files are located. These functions might contain reusable actions or routines.

- **outputDirectory**: Directory path where the output files, such as screenshots and logs, will be saved.

- **forceScreenshot**: Boolean value indicating whether screenshots should be captured forcibly during the test execution.

- **timeout**: Timeout duration in seconds for each action during the test execution.

- **sendEmail**: Boolean value indicating whether to send email alerts after the test execution.

- **emailRecipientList**: List of email addresses to which the email alerts will be sent.

- **userAgent**: User-Agent string to be used for web requests during the test execution.

- **visible**: Boolean value indicating whether the browser window should be visible during the test execution.

- **screenResolution**: Array specifying the screen resolution width and height in pixels.

- **actions**: List of actions to be executed sequentially during the test. Each action is represented as a string with the action keyword followed by its parameters.
