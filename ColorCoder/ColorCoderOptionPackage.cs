﻿using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using ColorCoder.Classifications;
using ColorCoder.ColorCoderCore;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ColorCoder
{
    [Guid(Guids.ChangeColorOptionGrid)]
    public class ChangeColorOptionGrid : DialogPage
    {
        private ColorManager _colorManager;

        private const string ColorSubCategory = "Colors";

        [Category(ColorSubCategory)]
        [DisplayName("Class")]
        public Color Class
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.Class); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.Class, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Delegate")]
        public Color Delegate
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.Delegate); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.Delegate, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Interface")]
        public Color Interface
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.Interface); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.Interface, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Struct")]
        public Color Struct
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.Struct); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.Struct, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Enum")]
        public Color Enum
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.Enum); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.Enum, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Generic Type Parameter")]
        public Color GenericTypeParameter
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.GenericTypeParameter); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.GenericTypeParameter, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Module(VB Only)")]
        public Color Module
        {
            get { return _colorManager.GetBuiltIn(ColorCoderClassificationName.Module); }
            set { _colorManager.SetBuiltIn(ColorCoderClassificationName.Module, value); }
        }

        //[Category(ColorSubCategory)]
        //[DisplayName("Attribute")]
        //public Color Attribute
        //{
        //    get { return _colorManager.Get(ColorCoderClassificationName.Attribute); }
        //    set { _colorManager.Set(ColorCoderClassificationName.Attribute, value); }
        //}

        [Category(ColorSubCategory)]
        [DisplayName("Local Variable")]
        public Color Local
        {
            get { return _colorManager.Get(ColorCoderClassificationName.LocalVariable); }
            set { _colorManager.Set(ColorCoderClassificationName.LocalVariable, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Enum Member")]
        public Color EnumMember
        {
            get { return _colorManager.Get(ColorCoderClassificationName.EnumMember); }
            set { _colorManager.Set(ColorCoderClassificationName.EnumMember, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Constructor (C# Only)")]
        public Color Constructor
        {
            get { return _colorManager.Get(ColorCoderClassificationName.Constructor); }
            set { _colorManager.Set(ColorCoderClassificationName.Constructor, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Field")]
        public Color Field
        {
            get { return _colorManager.Get(ColorCoderClassificationName.Field); }
            set { _colorManager.Set(ColorCoderClassificationName.Field, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Namespace")]
        public Color Namespace
        {
            get { return _colorManager.Get(ColorCoderClassificationName.Namespace); }
            set { _colorManager.Set(ColorCoderClassificationName.Namespace, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Method")]
        public Color Method
        {
            get { return _colorManager.Get(ColorCoderClassificationName.Method); }
            set { _colorManager.Set(ColorCoderClassificationName.Method, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Static Method")]
        public Color StaticMethod
        {
            get { return _colorManager.Get(ColorCoderClassificationName.StaticMethod); }
            set { _colorManager.Set(ColorCoderClassificationName.StaticMethod, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Extension Method")]
        public Color ExtensionMethod
        {
            get { return _colorManager.Get(ColorCoderClassificationName.ExtensionMethod); }
            set { _colorManager.Set(ColorCoderClassificationName.ExtensionMethod, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Property")]
        public Color AutomaticProperty
        {
            get { return _colorManager.Get(ColorCoderClassificationName.Property); }
            set { _colorManager.Set(ColorCoderClassificationName.Property, value); }
        }

        [Category(ColorSubCategory)]
        [DisplayName("Parameter")]
        public Color Parameter
        {
            get { return _colorManager.Get(ColorCoderClassificationName.Parameter); }
            set { _colorManager.Set(ColorCoderClassificationName.Parameter, value); }
        }

        public override void LoadSettingsFromStorage()
        {
            var dte = (EnvDTE80.DTE2)Package.GetGlobalService(typeof(SDTE));

            this._colorManager = new ColorManager(new ColorStorage(this.Site), dte);

            _colorManager.Load(
                //ColorCoderClassificationName.Attribute,
                ColorCoderClassificationName.Constructor,
                ColorCoderClassificationName.EnumMember,
                ColorCoderClassificationName.ExtensionMethod,
                ColorCoderClassificationName.Field,
                ColorCoderClassificationName.LocalVariable,
                ColorCoderClassificationName.Method,
                ColorCoderClassificationName.Namespace,
                ColorCoderClassificationName.Property,
                ColorCoderClassificationName.StaticMethod,
                ColorCoderClassificationName.Parameter
            );
        }

        public override void SaveSettingsToStorage() { }
    }

    [Guid(Guids.PresetOptionGrid)]
    public class PresetOptionGrid : DialogPage
    {
        private ColorManager _colorManager;

        private const string PresetSubCategory = "Presets";

        [Category(PresetSubCategory)]
        [DisplayName("Preset")]
        [Description("Select from one of the available sets of Presets")]
        public Preset Preset { get; set; }

        public override void LoadSettingsFromStorage()
        {
            var dte = (EnvDTE80.DTE2)Package.GetGlobalService(typeof(SDTE));

            this._colorManager = new ColorManager(new ColorStorage(this.Site), dte);

            Preset = Settings.Load().Preset;
        }

        public override void SaveSettingsToStorage()
        {
            Settings.Save(new PresetData { Preset = Preset });

            if (Preset == Preset.NoPreset) return;

            if (Preset == Preset.VisualStudioDefault)
            {
                _colorManager.RestoreBuiltInColorsToDefault();
                _colorManager.RestoreColorCoderToDefault();
            }

            if (Preset == Preset.ColorCoderDefault)
            {
                _colorManager.RestoreBuiltInColorsToDefault();
                var colors = _colorManager.GetColorableItemInfoDictionary();
                _colorManager.Save(colors);
            }

            if (Preset == Preset.ColorCoderExtreme)
            {
                var colors = _colorManager.GetColorableItemInfoDictionary();
                _colorManager.Save(colors);
                _colorManager.SetDefaultBuiltInColors();
            }
        }
    }

    [Guid(Guids.ColorCoderOptionPackage)]
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\14.0")]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideOptionPage(typeof(ChangeColorOptionGrid), "ColorCoder", "Colors", 1000, 1001, true)]
    [ProvideOptionPage(typeof(PresetOptionGrid), "ColorCoder", "Presets", 1000, 1001, true)]
    [InstalledProductRegistration("ColorCoder", "Color Coder provides semantic coloring for C# and VB - http://hamidmosalla.com/color-coder/", Vsix.Version, IconResourceID = 400)]
    public sealed class ColorCoderOptionPackage : Package { }
}