# Twitching of Isaac Streamer.bot Extension

Adds a way to call Twitching of Isaac commands from Streamer.bot.

## Download

[Right click here and use Save As to download "*Twitching of Isaac.sb*".](Twitching%20of%20Isaac.sb?raw=1)

## Auth Token

As this extension is in beta, please reach out to **sublimnl** on the [Twitching of Isaac Discord](https://discord.com/invite/5R9CSxzcep) to request your personal auth token. In the future this will be available on your Twitching of Isaac dashboard.

## Installation

1. In Streamer.bot click on **Import**
![](assets/sb_step1.png?raw=true)

2. Drag the "*Twitching of Isaac.sb*" file that you can download above, into Import String.
![](assets/sb_step2.png?raw=true)

3. Click the import button and two default/example actions will be created in a "Twitching of Isaac" group.

## Entering your Auth Token

The first time one of these actions are triggered, you will receive a pop up message to enter and validate your authentication token. See the Testing section below for how to test the actions, which will allow you to enter your token.

Alternatively, in Variables on Streamer.bot, you can create a new Persisted Global variable with the name "TOIAuthToken" and the value being the auth token you were provided.

## Testing

The default actions created will have a Test type Trigger created. You can right click on this trigger, and click Test Trigger.

If you have not entered your auth token yet, a popup form will allow you to enter the token. If you have previously entered your auth token, then the action should be triggered and the command will be sent to your Twitching of Isaac connected Binding of Isaac game.

## Adding Commands

By default, two example actions are provided. You can duplicate the existing ones to customize and create additional entries.

The `Fart` action will have a sub-action defined that sets the argument %command% to 'fart'. You can use this as a template to create calls for any command that does not expect additional input.

The `Give 20/20` action has two sub-actions for setting two different arguments; %command% in this case is the `give` command, while the second parameter set is %parameters% which allows you to specify the input after the command, in this case `20/20`.

## Help

If you need any assistance, or have any bugs to report, please join the [Twitching of Isaac Discord](https://discord.com/invite/5R9CSxzcep).
