# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define am = Character('Answering Machine', color="#ff8800")
define dad = Character('Dad', color="#cc5500")

# The game starts here.
label start:
    play voice "newmessage.mp3"
    am "You have one new message."
    pause 1.0

    play voice "Dad-1.mp3"
    dad "Hey, kiddo. So, uh... did you get your birthday present? I found it under the couch... cleaned it up a bit."

    play voice "Dad-2.mp3"
    dad "What’d you call it? Hamburger or something like that? You always kept it with you. Anyway, it’s been a long time, hasn’t it?"

    play voice "Dad-3.mp3"
    dad "I thought maybe you’d like to meet up at a park, get some coffee or something? You know, I ain’t that far outta town..."

    play voice "Dad-4.mp3"
    dad "Well, I hope to hear from you soon, kiddo. Tell your mom I miss her, will ya? Congratulations on your new house. I bet it’s real fancy. Proud of you. Bye."
    pause 1.0

    play voice "savemessage.mp3"
    am "Would you like to save this message?"
    pause 1.0

label end:
    return
