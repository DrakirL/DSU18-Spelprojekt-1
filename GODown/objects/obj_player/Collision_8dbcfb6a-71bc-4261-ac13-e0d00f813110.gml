if(keyboard_check_pressed(vk_space))
{
	with(other)
	{
		if(!carried)
			carried = true;
		else
			carried = false;
	}
}