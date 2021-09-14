# NiceLocomotion
A nice locomotion that switches from Teleportation to Continuous Movement and vice versa seamlessly.

## Left Hand Controls
1. Press the left thumbstick up to summon a destination point. A ray will show up to indicate the destination point.
2. When the left thumbstick is released, the user will be teleported to the destination point.
3. To switch to Continuous Movement, press the grip button as well as the thumbstick. The grip button turns on the Continuous Movement component, so as long as it is pressed, the user will be able to use the left hand controller to move.
4. To switch back to teleportation, release the grip button.

## Right Hand Controls
1. Use the right thumbstick to snap rotate.
## How to use
Either download the whole repo or just attach the TeleportManager.cs script to the VR Rig in the current scene.
### Setting up XR Interaction Toolkit
1. Download the XR Interaction Toolkit from the package manager.
2. Notice that you must "Enable Preview Package" from project settings to do that.
### Setting up VR Rig
1. Create an XR Rig Device-Based object in the scene.
2. Create a Locomotion System as a child of the XR Rig Device-Based.
3. Add the TeleportManager.cs script to the Locomotion System.
4. Assign XRI Default Input Actions to the "Action Asset" field.
5. Assign Ray Interactor to the preferred controller. In this case, the left hand ray interactor.
6. Assign the Locomotion system to the teleportation provider and continuous move provider fields.
### Setting up the scene
1. Add the Teleportation Area component to every object in the scene that the user can teleport to. For instance, the floor. 
2. Asssign the locomotion system to the Teleportation Provider field of the Teleportation Area component.

### Dislcaimer
1. I assume to use left hand to move and right hand to rotate. This setting can be changed. It works either way.
2. I think if one downloads the whole repo and runs it on Unity, there is nothing else to be done. It will run fine.
