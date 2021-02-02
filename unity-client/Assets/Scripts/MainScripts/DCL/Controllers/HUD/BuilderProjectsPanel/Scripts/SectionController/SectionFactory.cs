﻿using System;

internal interface ISectionFactory
{
    SectionBase GetSectionController(SectionsController.SectionId id);
}

internal class SectionFactory : ISectionFactory
{
    SectionBase ISectionFactory.GetSectionController(SectionsController.SectionId id)
    {
        SectionBase result = null;
        switch (id)
        {
            case SectionsController.SectionId.SCENES_MAIN:
                result = new SectionScenesController();
                break;
            case SectionsController.SectionId.SCENES_DEPLOYED:
                result = new SectionDeployedScenesController();
                break;
            case SectionsController.SectionId.SCENES_PROJECT:
                result = new SectionProjectScenesController();
                break;
            case SectionsController.SectionId.LAND:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }

        return result;
    }
}
