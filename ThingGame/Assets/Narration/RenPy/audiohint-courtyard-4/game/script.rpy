# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define tt = Character('The Thing', color="#ff0000")

# The game starts here.
label start:
    play voice "Thing_The_.mp3"
    tt "The..."

label end:
    return
