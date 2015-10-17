
all:
	fsharpc -r Suave.dll Server.fs

stand:
	fsharpc -r Suave.dll Server.fs --standalone
