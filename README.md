# ECurvesToolbox

`CurveToolbox` is a comprehensive utility for managing animation curves in Unity. It provides tools for adding, removing, renaming, and merging animation curves, allowing you to streamline your animation workflow.

![ECurvesToolbox Window](/ECurvesToolbox.png)

## Features

### Add Curves
Easily add curves from one animation clip to another, making it simple to reuse and modify existing animations.

### Remove Curves
Efficiently remove specific curves from animation clips, including options to remove position, rotation, and scale curves. You can also include child objects and modify the curve hierarchy.

### Rename Curves
Rename curves within animation clips to maintain a consistent naming convention across your animations. This feature includes options to rename specific axes and supports batch renaming.

### Merge Clips
Combine multiple animation clips into a single clip, optimizing your animations and reducing the number of assets you need to manage.

## Why You Need It

Managing animation curves in Unity can be a tedious task, especially when dealing with complex rigs and multiple animations. `CurveToolbox` simplifies this process by providing a set of powerful tools to handle common tasks, improving your workflow and allowing you to focus on creating high-quality animations.

## How to Use

### Add Curves

1. **Open CurveToolbox**: Navigate to `Escripts/Curve Toolbox` in the Unity menu.
2. **Select Add Curves Tab**: Choose the "Add Curves" tab.
3. **Specify Clips**: Assign the source and target animation clips.
4. **Add Curves**: Click the "Add Curves" button to transfer curves from the source clip to the target clip.

### Remove Curves

1. **Select Remove Curves Tab**: Choose the "Remove Curves" tab.
2. **Set Parameters**: Specify the curve path, include children option, and types of curves to delete.
3. **Remove Curves**: Click the "Delete Curves" button to remove the specified curves from selected clips.

### Rename Curves

1. **Select Rename Curves Tab**: Choose the "Rename Curves" tab.
2. **Specify Names**: Enter the old and new names for the curves.
3. **Set Options**: Choose whether to rename all curves or specific types (scale, rotation, position).
4. **Rename Curves**: Click the "Rename Curves" button to apply the changes.

### Merge Clips

1. **Select Merge Clips Tab**: Choose the "Merge Clips" tab.
2. **Set Save Path and Name**: Define the save path and name for the merged clip.
3. **Add Clips**: Add the animation clips you want to merge.
4. **Select Priority Clip**: If applicable, select a priority clip.
5. **Merge Clips**: Click the "Merge Clips" button to combine the clips into one.
