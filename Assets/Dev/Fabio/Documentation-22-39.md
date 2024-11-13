# Documentation for issue [#22](https://github.com/orgs/SillyBusinessInc/projects/1/views/1?pane=issue&itemId=86093891&issue=SillyBusinessInc%7CSillyBusinessGame%7C22)

# Documentation for issue [#37](https://github.com/orgs/SillyBusinessInc/projects/1/views/1?pane=issue&itemId=86744328&issue=SillyBusinessInc%7CSillyBusinessGame%7C39)

## Global Reference

### Requirements:
To create a new reference object make sure the following applies:
- The object always exists and does not get removed/disabled during the lifespan of the scene
- The object always exists only once per scene

### How to
Create a new script in the folder -> Assets/Global/Scripts/Reference/GameObjects
Make sure the class inherits from Reference and it's name ends with Reference.
Add any properties you want to be quickly accassible to this class.
Drag the reference script onto the object you want to be referenced.

Now from anywhere in the project you can acces this script with GlobalReference.GetReference<_ReferenceScript_>();

## Events

## Creating an event
Events are automatically created when either invoked or subscribed to.
Make sure you add the event name to the Events enum in ->Assets/Global/Scripts/Reference/Events.cs

## Invoking an event
You can invoke an by calling -> GlobalReference.AttemptInvoke(Event);

## Subscribing to an event
You can subscribe to an event by calling: GlobalReference.SubscribeTo(Event);

## Unsubscribing to an event
You can subscribe to an event by calling: GlobalReference.UnsubscribeTo(Event);