@file:Suppress("EXPERIMENTAL_API_USAGE","EXPERIMENTAL_UNSIGNED_LITERALS","PackageDirectoryMismatch","UnusedImport","unused","LocalVariableName","CanBeVal","PropertyName","EnumEntryName","ClassName","ObjectPropertyName","UnnecessaryVariable","SpellCheckingInspection")
package com.jetbrains.rider.model

import com.jetbrains.rd.framework.*
import com.jetbrains.rd.framework.base.*
import com.jetbrains.rd.framework.impl.*

import com.jetbrains.rd.util.lifetime.*
import com.jetbrains.rd.util.reactive.*
import com.jetbrains.rd.util.string.*
import com.jetbrains.rd.util.*
import kotlin.reflect.KClass



/**
 * #### Generated from [XamlStylerModel.kt:8]
 */
class XamlStylerModel private constructor(
    private val _myString: RdOptionalProperty<String>
) : RdExtBase() {
    //companion
    
    companion object : ISerializersOwner {
        
        override fun registerSerializersCore(serializers: ISerializers)  {
        }
        
        
        
        
        const val serializationHash = 5726963228597487203L
        
    }
    override val serializersOwner: ISerializersOwner get() = XamlStylerModel
    override val serializationHash: Long get() = XamlStylerModel.serializationHash
    
    //fields
    val myString: IOptProperty<String> get() = _myString
    //methods
    //initializer
    init {
        _myString.optimizeNested = true
    }
    
    init {
        bindableChildren.add("myString" to _myString)
    }
    
    //secondary constructor
    internal constructor(
    ) : this(
        RdOptionalProperty<String>(FrameworkMarshallers.String)
    )
    
    //equals trait
    //hash code trait
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("XamlStylerModel (")
        printer.indent {
            print("myString = "); _myString.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    override fun deepClone(): XamlStylerModel   {
        return XamlStylerModel(
            _myString.deepClonePolymorphic()
        )
    }
    //contexts
}
val Solution.xamlStylerModel get() = getOrCreateExtension("xamlStylerModel", ::XamlStylerModel)

