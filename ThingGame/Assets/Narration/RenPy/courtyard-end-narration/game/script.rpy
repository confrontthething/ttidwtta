# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define e = Character('Frinchfry', color="#c8ffc8")


# The game starts here.
label start:
    play voice "ff-hubworld2-7.wav"
    e "You better get ready for us, cause we're coming! Onward to the Throne Room!"
label end:
    return
