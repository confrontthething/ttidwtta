# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define p = Character('Psychologist', color="#c8a2c8")

# The game starts here.
label start:

    play voice "doorknock.mp3"

    pause 2.0

    play voice "p-line1.mp3"
    p "Hey, group session is in fifteen."
    pause 1.0

    play voice "p-line2.mp3"
    p "I hope to see you there again today."
    pause 1.0

    play voice "p-line3.mp3"
    p "I heard your father sent a gift for your birthday. Maybe you’d like to talk about that?"
    pause 1.0

    play voice "p-line4.mp3"
    p "So, I’ll see you soon. You can bring the bear if you’d like."

label end:
    return
