PL.Net
======

This is a .Net wrapper for the new Planimate® DLL interface which enables Planimate® to be embedded and controlled in .Net projects. Code is written in c# and VS2013 projects are provided.
API documentation for the wrapper is available [here](http://interdynamics.github.io/PL.Net/)


The Planimate engine itself is not included within this repository. Users must hold a Level 4 license of Planimate® to be able to compile dll engines for use within this .Net wrapper. Please contact [InterDynamics](http://www.interdynamics.com/contact-us/) for more infomation.


The demo application included (WindowsFormsApplication1) assumes you have a Planimate® engine available and have the engine in the binary target directories for the project. (WindowsFormsApplication1/bin/[Release|Debug]
The project also assumes the engine is named planimate.dll
