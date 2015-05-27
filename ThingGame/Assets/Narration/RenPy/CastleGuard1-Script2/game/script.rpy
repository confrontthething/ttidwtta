# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define c1 = Character('Castle Guard 2', color="#0000ff")
define ff = Character('Frinchfry', color="#c8ffc8")


# The game starts here.

label start:

label line1:
    play voice "c3-line1.wav"
    c1 "I heard you were back..."
    pause 0.4
    
label line2:
    play voice "c3-line2.wav"
    c1 "Sorry to break it to you, but there’s more of those things."
    pause 0.4
    
label line3:
    play voice "c3-line3.wav"
    c1 "To get rid of them all, I guess you need to get rid of the source."
    pause 0.4
    
label line4:
    play voice "c3-line4.wav"
    c1 "I don’t know where it is. Maybe King Grumpybeard knows?"
    pause 0.4
    
    label line5:
    play voice "c3-line5.wav"
    c1 "Go to the throne room... You remember where that is right? Start by taking those stairs outta this dungeon!"
    pause 0.4
label end:
    return
