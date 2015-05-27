# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define e = Character('The Thing', color="#ff0000")


# The game starts here.
label start:
    play voice "Thing_All_.mp3"
    e "All this running away..."

    play voice "Thing_Must_.mp3"
    e "...must get heavy on your shoulders."

    play voice "Thing_Find_.mp3"
    e "Find me and end this."

label end:
    return
