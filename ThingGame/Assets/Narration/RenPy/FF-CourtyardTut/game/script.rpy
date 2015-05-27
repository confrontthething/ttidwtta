# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")

# The game starts here.
label start:
    play voice "ff-courtyard-2.wav"
    ff "Yep, we’re on the right track."
    pause 0.5
    play voice "ff-courtyard-1.wav"
    ff "See those red vines? Shoot them and watch ‘em burnnn baby burn!"
    pause 0.2
label end:

    return
