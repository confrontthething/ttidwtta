# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define c1 = Character('Castle Guard 1', color="#c8ffc8")
define ff = Character('Frinchfry', color="#c8ffc8")


# The game starts here.

label start:
    
label line1:
    play voice "c1-line1.wav"
    c1 "Oh. Hey there. Wow, I was not expecting you. It’s been a long time. Where are my manners? I guess I have the keys don’t I, haha."
    pause 0.2

label line2:
label raiseBars:
    play voice "c1-line2.wav"
    c1 "Man, I miss the good old days when I’d open your cell and say “Here’s your food, swine.”"
    pause 0.2
    play voice "c1-line4-final.wav"
    c1 "Remember that? Then you’d swing your staff and “poof,” gone. Good times... good times."
    pause 0.2

label line3:
    play voice "c1-line5-final.wav"
    c1 "Oh yeah... one more thing. That SHADOW THING flew through here. It left behind little pieces of something. I dunno, but I don’t like them. Watch out and good luck."
label end:
label openCellRoomDoor:
    return
