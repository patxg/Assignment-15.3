# 🎮 Character Animation System

A modular and scalable **Character Animation System** for Unity that handles smooth state transitions, movement, jumping, combat, and interaction animations.

---

## 🚀 Features

### 🎮 Character Controls
- **Walking & Running** (`WASD + Shift`)
- **Jumping & Falling** (`Space / W`)
- **Crouching & Sliding** (`S / Ctrl`)
- **Dashing** (`Ctrl` when running)
- **Attacking & Blocking** (`LMB / RMB`)
- **Reloading & Weapon Swap** (`R / Q`)
- **Interacting & Using Items** (`E / F`)
- **Inventory & Pause Menu** (`Tab / Escape`)

### 🎬 Animation System
- **Automated Animator Setup** (`AnimatorSetup.cs`)
- **State Machine-Driven Animation Updates** (`CharacterAnimationSystem.cs`)
- **ScriptableObject for Animation Data** (`CharacterAnimationConfig.cs`)
- **Dynamic Transitions Without Duplicates** (`CharacterController2D.cs`)

### 🛠 Editor Utility
- **Auto-Update Animation Clips from a Folder** (`CharacterAnimationUpdater.cs`)

---

## 📂 Scripts
/Assets/Scripts/ 
- CharacterAnimationConfig.cs # ScriptableObject storing animation clips
- CharacterAnimationSystem.cs # Controls animation transitions 
- AnimatorSetup.cs # Automatically sets up Animator states
- CharacterAnimationUpdater.cs # Editor script for batch animation updates 
-  CharacterController2D.cs # Handles movement, jumping, and dashing
- CharacterState.cs # Enum defining character animation states

---

## ⚙ Setup Instructions

### 1️⃣ Attach Components
1. **Add `CharacterController2D.cs`** to the Player GameObject.
2. **Add `CharacterAnimationSystem.cs`** to the Player GameObject.
3. **Assign an `Animator` component** to the Player with **`CharacterAnimatorController`**.

### 2️⃣ Configure Animator Parameters
1. **Open Unity's Animator (`Ctrl + 6`)**.
2. **Add the following parameters**:
   - `Speed` (**Float**)
   - `VelocityY` (**Float**)
   - `IsGrounded` (**Bool**)
   - `IsDashing` (**Bool**)
   - `IsRunning` (**Bool**)
   - `IsAttacking` (**Bool**)

### 3️⃣ Run the Animator Setup
- **Go to `Tools > Run Animator Setup`** to automatically add missing states and transitions.

### 4️⃣ Assign Animation Clips
1. **Create `CharacterAnimationConfig`**:
   - `Right Click > Create > Character > AnimationConfig`
2. **Assign Animation Clips** in the **Inspector**.

---

## 📜 Character State System

The **`CharacterState.cs`** file defines **all possible character states**:

```
public enum CharacterState
{
    Idle,
    Walking,
    Running,
    Sprinting,
    Jumping,
    JumpFall,
    Landing,
    Crouching,
    Rolling,
    Dashing,
    Attacking,
    Blocking,
    Reloading,
    SwappingWeapon,
    Interacting,
    UsingItem,
    OpeningInventory,
    Paused
}
```
### 🔄 Customization
   - Adding a New Animation
   - Add a new AnimationClip in CharacterAnimationConfig.cs.
   - Update CharacterState.cs with a new enum value.
   - Modify CharacterAnimationSystem.cs:
```
case CharacterState.MyNewState:
    animator.SetTrigger("MyNewAnimationTrigger");
    break;
```
   - Manually add transitions in the Animator or rerun AnimatorSetup.cs.
   - Adding Custom Inputs
   - Modify CharacterController2D.cs:

```
if (Input.GetKeyDown(KeyCode.X)) 
{
    animationSystem.ChangeState(CharacterState.MyNewState);
}
```
### 🛠 Known Issues & Fixes
1️⃣ Character Doesn't Move?
   - Ensure Rigidbody2D is added.
   - Enable Freeze Rotation Z in Rigidbody2D.
   - Ensure ground detection works using groundCheck.
2️⃣ Walking Animation Doesn't Stop?
   - Ensure Speed resets to 0 when no movement:
```
animationSystem.SetAnimatorParameter("Speed", 0f);
animationSystem.ChangeState(CharacterState.Idle);
```
3️⃣ Jumping Animation Doesn't Trigger?
   - Ensure VelocityY updates properly:
```
animationSystem.SetAnimatorParameter("VelocityY", jumpForce);
```
4️⃣ Character Faces Backwards When Moving Left?
   - Ensure the Flip() function in CharacterController2D.cs correctly inverts transform.localScale.x.

