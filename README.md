I made a matrix code effect in console because why not?

I instantiate a CodeStream object and move it down. The CodeStream object handles the chars that'll be displayed.
The fun part here is rendering in the console. Trying to render 1 char at a time is waaay too slow. 
So I render first in a buffer, then render each line via a string builder.
