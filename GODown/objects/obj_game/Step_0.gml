if(keyboard_check_pressed(vk_right))
{
		global.currentGravityDirection = RIGHT;
		gravityShifts++;
}
if(keyboard_check_pressed(vk_up))
{
		global.currentGravityDirection = UP;
		gravityShifts++;
}
if(keyboard_check_pressed(vk_left))
{
		global.currentGravityDirection = LEFT;
		gravityShifts++;
}
if(keyboard_check_pressed(vk_down))
{
		global.currentGravityDirection = DOWN;
		gravityShifts++;
}

if(keyboard_check_pressed(vk_tab) && room != rm_win)
{
	if(obj_stats.visible)
		obj_stats.visible = false;
	else
		obj_stats.visible = true;
}

if keyboard_check_pressed(ord("N"))
{
	if(room != rm_win)
		room_goto_next();
}

if keyboard_check(ord("P"))
{
	if(room != rm_level0)
		room_goto_previous(); //Whatever...
}