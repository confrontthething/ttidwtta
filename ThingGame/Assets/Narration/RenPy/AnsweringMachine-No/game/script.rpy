# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define am = Character('Answering Machine', color="#ff8800")
define dad = Character('Dad', color="#cc5500")

# The game starts here.
label start:
    play voice "messagedeleted.aiff"
    am "Message deleted."

label end:
    return
