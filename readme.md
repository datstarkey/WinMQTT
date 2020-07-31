# WinMQTT

WinMQTT is a windows client to control windows remotely via MQTT.

# Setup

Install from the installer in the Zip file.

Enter your mqtt broker IP and port.
(username and password aren't working yet)

control windows remotely.

# Commands

## Actions

There are several ections you can send to

| Command                   | Description                    |
| ------------------------- | ------------------------------ |
| windows/actions/sleep     | Send windows to sleep          |
| windows/actions/hibernate | Hibernate windows              |
| windows/actions/shutdown  | Shutdown windows               |
| windows/actions/restart   | Restart computer               |
| windows/actions/logout    | Log out of current user        |
| windows/actions/lock      | Lock current user              |
| windows/actions/exit      | Closes the WinMQTT application |

## Windows Status

Send any message to the topic

```
   windows/status
```

Will return a true if the mqtt client is connected (and therefore the windows is on) to the topic

```
  windows
```

## Volume

Change windows volume by sending a value between 0-100, this topic will also return the current volume if state requested

```
    windows/volume
```

Request the current volume by sending any message to

```
   windows/volume/state
```

## Sendkeys

Send a message to type that message, can be used for global hotkeys, uses the WinAPI for SendKeys

https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=netframework-4.8

```
   windows/sendkeys
```

## Screens

Send any message to the topic

```
   windows/screens/status
```

Will return a number with how many monitors are active on

```
  windows/screens
```
