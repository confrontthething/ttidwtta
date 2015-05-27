# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define e = Character('The Thing', color="#ff0000")

# The game starts here.
label start:
    play voice "Thing_DontYou_.mp3"
    e "Don't you know by now... What I am?"


label end:
    return
