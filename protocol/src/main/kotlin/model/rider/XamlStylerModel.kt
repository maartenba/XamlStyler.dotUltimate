package model.rider

import com.jetbrains.rider.model.nova.ide.SolutionModel
import com.jetbrains.rd.generator.nova.*
import com.jetbrains.rd.generator.nova.PredefinedType.*

@Suppress("unused")
object XamlStylerModel : Ext(SolutionModel.Solution) {

    init {
        //setting(CSharp50Generator.Namespace, "ReSharperPlugin.XamlStyler.dotUltimate.Rider.Model")
        //setting(Kotlin11Generator.Namespace, "com.jetbrains.rider.xamlstyler.dotultimate.model")

        property("myString", string)
    }
}